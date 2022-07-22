using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        public IEnumerable<Member> GetMembers();
        public Member GetMemberByEmail(String memberEmail);
        public void AddNewMember(Member member);
        public void UpdateMember(Member member);
        public bool RemoveMember(String memberEmail);

        public Member Login(string Email, string Password);

    }
}
