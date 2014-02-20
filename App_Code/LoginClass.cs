using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for LoginClass
/// </summary>
public class LoginClass
{
    AutomartEntities1 ae = new AutomartEntities1();
    string password;
    string userName;

	public LoginClass(string pass, string user)
	{
        password = pass;
        userName = user;
	}

    public int ValidateLogin()
    {
        //personkey to return initally 0
        int pk = 0;

        //LINQ to extract personkey, passcode and hash from database
        var log = from r in ae.RegisteredCustomers
                  where r.Email == userName
                  && r.CustomerPassword == password
                  select new { r.PersonKey, r.CustomerPassCode, r.CustomerHashedPassword};
        //variables to store results from database
        int pCode=0;
        Byte[] pWord=null;
        int personKey=0;

        //loop throug results and assign values from var log
        //to our variables
        foreach (var p in log)
        {
            personKey = (int)p.PersonKey;
            pCode = (int)p.CustomerPassCode;
            pWord = (Byte[])p.CustomerHashedPassword;
        }
        //initial the PassWordHash
        PasswordHash ph = new PasswordHash();
        //send password and passcode to be hashed
        Byte[] newHash = ph.Hashit(password,pCode.ToString());
     

        if (pWord.SequenceEqual(newHash))
        {
            pk = personKey;
        }
        return pk;
    }
   
}