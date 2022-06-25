using UnityEngine;
using TMPro;

public class loginScript : MonoBehaviour
{
    [SerializeField] TMP_InputField text;
    [SerializeField] TMP_InputField passwordText;
    private string username;
    private string userpassword;
    private bool check;

    private void checkInputUsername()
    {
        if (text.text != null && text.text != "Enter Discord Name")
        {
            check = true;
            username = text.text;
        }
        else check = false;
    }

    private void checkInputPassword()
    {
        if (passwordText.text != null && text.text != "Password Here")
        {
            check = true;
            userpassword = passwordText.text;
        }
        else check = false;
    }

    public string[] usernamePassword()
    {
        string[] errorMsg = { "error", "username/password is incorrect" };
        checkInputUsername();
        checkInputPassword();
        if (check)
        {
            string[] temp = { username, userpassword };
            return temp;
        } else return errorMsg;
    }
}
