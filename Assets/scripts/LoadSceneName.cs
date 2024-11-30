using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneName : MonoBehaviour

{
        public TMP_Text txtError;
        public TMP_InputField txtUsuario;
        private IEnumerator coroutine;
    
    public void BackToLoad()
    {
        SceneManager.LoadScene(Resource.Screen.Load);
    }
    public void DigiteNome()
    {
        UserCreateRequest userCreateRequest = new UserCreateRequest
        {
            username = txtUsuario.text,
        };

        var validation = userCreateRequest.Validate();

        if (string.IsNullOrEmpty(validation))
        {
            coroutine = NetworkService.CreateUser(userCreateRequest, txtError);
            StartCoroutine(coroutine);
        }
        else
        {
            txtError.text = validation;
        }
    }
}

public class InitiateScreenController : MonoBehaviour
{
    private byte transparencia;
    public SpriteRenderer image;

    void Start()
    {
        transparencia = 0;
    }

    void FixedUpdate()
    {
        if (transparencia < 250)
        {
            transparencia += 10;
            image.color = new Color32(255, 255, 255, transparencia);
        }
        else
        {
            if (PlayerPrefs.GetString(Resource.APIKey) != "")
                SceneManager.LoadScene(Resource.Screen.Principal);
            else
                SceneManager.LoadScene(Resource.Screen.Login);
        }
    }
}