using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginAuthentication : MonoBehaviour
{
    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private GameObject failed;
    [SerializeField] private GameObject success;
    Firebase.Auth.FirebaseAuth auth;
    private void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    public void OnSignIn()
    {
        StartCoroutine(SignIn());
    }

    public void OnSignUp()
    {
        StartCoroutine(SignUp());
    }
    IEnumerator SignIn()
    {
        var registeredTask = auth?.SignInWithEmailAndPasswordAsync(email.text, password.text);
        yield return new WaitUntil(()=>registeredTask.IsCompleted);
        if (registeredTask.IsCompletedSuccessfully)
        {
            Debug.Log("Success");
            success.SetActive(true);
            StartCoroutine(PanelDeactivate(success));
            SceneManager.LoadScene(1);
        }
        else
        {
            failed.SetActive(true);
            StartCoroutine(PanelDeactivate(failed));
            Debug.Log("Failed");
        }
           

    }

    IEnumerator SignUp()
    {
        var registeredTask = auth?.CreateUserWithEmailAndPasswordAsync(email.text, password.text);
        yield return new WaitUntil(() => registeredTask.IsCompleted);
        if (registeredTask.IsCompletedSuccessfully)
        {
            success.SetActive(true);
            StartCoroutine(PanelDeactivate(success));
            Debug.Log("Success");
            SceneManager.LoadScene(1);
        }
        else
        {
            failed.SetActive(true);
            StartCoroutine(PanelDeactivate(failed));
            Debug.Log("Failed");
        }
    }

    IEnumerator PanelDeactivate(GameObject panel)
    {
        yield return new WaitForSeconds(2.5f);
        panel.SetActive(false);
    }

}
