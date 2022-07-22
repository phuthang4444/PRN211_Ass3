using BusinessObject;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public void AddNewMember(Member member)
            => MemberDAO.Instace.AddNew(member);

        public Member GetMemberByEmail(string memberEmail)
            => MemberDAO.Instace.GetMemberByEmail(memberEmail);

        public IEnumerable<Member> GetMembers()
            => MemberDAO.Instace.GetMemberList();

        public Member Login(string Email, string Password)
            => MemberDAO.Instace.Login(Email, Password);

        public bool RemoveMember(string memberEmail)
            => MemberDAO.Instace.Remove(memberEmail);

        public void UpdateMember(Member member)
            => MemberDAO.Instace.Update(member);
    }
}
