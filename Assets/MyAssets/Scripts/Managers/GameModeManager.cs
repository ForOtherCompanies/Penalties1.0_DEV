using UnityEngine;
using System.Collections;

  //esta clase ofrece funciones para que sean llamadas desde el GuiManager ( desde la propia GUI). Las funciones de aqui 
//// se encargan de orquestar
//// los cambios de modo y asi liberar de trabajo al guiManager (que hara solo de enlace entre la gui y esto). Recordar
//// que tiene que orquestar menus de hasta tres niveles de profundidad (principal->entrenamiento->etrenamientoEntandar por ejemplo)
//// de modo que necesitara tener punteros a todo lo necesario para activar/desactivar interfaces y lo mismo para activar/desactivar
//// el script de cada modo de juego Y tambien inicializarlos convenientemente.
//// Probablemente tambien sea el sitio apropiado para reapuntar InputManager.GameManager al script que pase a modo activo
//// ya que desde aqui los tendremos todos correctamente referenciados
public class GameModeManager : MonoBehaviour {
	//keep public
	//punteros a los distintos game modes (cada uno un gameobjecto completo o solo el script?)
	public EntrenamientoDianas entrenamientoDianas;
	public EntrenamientoEstandar entrenamientoEstandar;
	public EntrenamientoParadas entrenamientoParadas;
	public MultiPlayerGController multiplayerGame;
	//game objects necesarios
	public PelotaGameRules pelotaRules;

	//to set private
	public GameModeVirtual currentGameMode;

	public void Start (){
		//entrenamientoEstandar.enabled = true;
		//entrenamientoDianas.enabled = false;
		//entrenamientoParadas.enabled = false;
	}


	public void ActivateEntrenamientoDianas(){
		DisableCurrentMode();
		currentGameMode = entrenamientoDianas;
		currentGameMode.enabled = true;

	}

	public void ActivateEntrenamientoEstandar(){
		DisableCurrentMode();
		currentGameMode = entrenamientoEstandar;
		currentGameMode.enabled = true;

		pelotaRules.currentGameMode = currentGameMode;
		
	}

	public void ActivateMultplayer(){
		DisableCurrentMode();
		currentGameMode = multiplayerGame;
		currentGameMode.enabled = true;
		
		pelotaRules.currentGameMode = currentGameMode;
		
	}

	public void ActivateEntrenamientoParadas(){
		DisableCurrentMode();
		currentGameMode = entrenamientoParadas;
		currentGameMode.enabled = true;		
	}



	public void DisableCurrentMode(){
		if (currentGameMode != null)
			currentGameMode.enabled = false;
	}



}
