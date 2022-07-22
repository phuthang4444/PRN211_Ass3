using System;
using System.Collections.Generic;
using System.Data;
using DataAccess.DataProvider;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using System.IO;
using Microsoft.Extensions.Configuration;
using BusinessObject.Models;

namespace DataAccess
{
    public class MemberDAO: BaseDAL
    {
        private static MemberDAO instance = null;

        private static readonly object instancelock = new object();

        private MemberDAO() { }

        public static MemberDAO Instace
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Member> MemberList = new List<Member>();

        private Member getAdmin()
        {
            Member admin = null;
            using (StreamReader r = new StreamReader("appsettings.json"))
            {
                string json = r.ReadToEnd();
                IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", true, true)
                                        .Build();
                string email = config["AdminAccount:Email"];
                string password = config["AdminAccount:Password"];

                admin = new Member
                {
                    MemberId = 0,
                    CompanyName = "F Store",
                    Email = email,
                    Password = password,
                    City = "",
                    Country = ""
                };
                return admin;
            }
        }

        public Member Login(string email, string password)
        {
            Member admin = getAdmin();
            if (admin.Email.Equals(email)
                &&
                admin.Password.Equals(password))
            {
                return admin;
            }
            else
            {
                MemberList = (List<Member>)GetMemberList();
                Member memberLogin = MemberList.SingleOrDefault(member => member.Email.Equals(email) && member.Password.Equals(password));
                return memberLogin;
            }
        }

        public IEnumerable<Member> GetMemberList()
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select MemberID, Email, CompanyName, City, Country, Password " +
                "From Member";
            var members = new List<Member>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    members.Add(new Member
                    {
                        MemberId = dataReader.GetInt32(0),
                        Email = dataReader.GetString(1),
                        CompanyName = dataReader.GetString(2),
                        City = dataReader.GetString(3),
                        Country = dataReader.GetString(4),
                        Password = dataReader.GetString(5)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return members;
        }
        public Member GetMemberByEmail(string memberEmail)
        {
            Member member = null;
            IDataReader dataReader = null;
            string SQLSelect = "Select MemberID, Email, CompanyName, City, Country, Password "
                + "  From Member" +
                " Where Email = @Email";
            try
            {
                var param = dataProvider.CreateParameter("@Email", 4, memberEmail, DbType.String);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    member = new Member
                    {
                        MemberId = dataReader.GetInt32(0),
                        Email = dataReader.GetString(1),
                        CompanyName = dataReader.GetString(2),
                        City = dataReader.GetString(3),
                        Country = dataReader.GetString(4),
                        Password = dataReader.GetString(5)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return member;
        }

        public void AddNew(Member member)
        {
            try
            {
                Member addMem = GetMemberByEmail(member.Email);
                if (addMem == null)
                {
                    string SQLInsert = "Insert Member " +
                        "values(@Email,@CompanyName,@City,@Country,@Password)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@Email", 100, member.Email, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@CompanyName", 40, member.CompanyName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@City", 15, member.City, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Country", 15, member.Country, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Password", 30, member.Password, DbType.String));
                    dataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("Member exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Update(Member member)
        {
            Member memUpdate = GetMemberByEmail(member.Email);

            try
            {
                if (memUpdate != null)
                {
                    string SQLUpdate = "Update Member" +
                        " set Email = @Email," +
                            " CompanyName = @CompanyName" +
                            " City = @City" +
                            " Country = @Country" +
                            " Password = @Password" +
                        " Where Email = @Email";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@Email", 100, member.Email, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@CompanyName", 40, member.CompanyName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@City", 15, member.City, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Country", 15, member.Country, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Password", 30, member.Password, DbType.String));
                    dataProvider.Insert(SQLUpdate, CommandType.Text, parameters.ToArray());

                }
                else
                {
                    throw new Exception("Member exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool Remove(string memberEmail)
        {
            Member memRemove = null;
            try
            {
                memRemove = GetMemberByEmail(memberEmail);

                if (memRemove != null)
                {
                    string SQLDelete = "Delete Member from Member" +
                        " Where Email = @Email";

                    var param = dataProvider.CreateParameter(SQLDelete, 4, CommandType.Text, DbType.Int32);
                    dataProvider.Delete(SQLDelete, CommandType.Text, param);
                    return true;
                }
                else
                {
                    throw new Exception("Member exists.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return false;
        }
    }
    
}
