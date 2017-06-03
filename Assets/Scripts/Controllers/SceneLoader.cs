//-----------------------------------------------------------------------
// <copyright file="SceneLoader.cs"> 
// Copyright (c) 2017 Todos los derechos reservados.
// </copyright>
// <summary>Clase SceneLoader.</summary>
// <author>Camilo Romero</author> 
//-----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que gestiona el cambio de escenas
/// </summary>
public class SceneLoader : MonoBehaviour
{

    #region Metodos

    static public SceneLoader instance;

    void Start(){
        instance = this;
    }

	/// <summary>
	/// Cargar Escena
	/// </summary>
	/// <param name="sceneName">Nombre de la escena.</param>
	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	#endregion

}
