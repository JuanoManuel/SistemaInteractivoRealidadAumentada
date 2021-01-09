using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{

	public Sprite[] images;
	public GameObject fichaPrfb;
	public GameObject bordePrfb;
	public GameObject mensajeGanador;
	public bool barajear;
	int numFichasLado;
	List<Sprite> fichaImg = new List<Sprite>();
	Sprite fichaEscondidaImg;
	GameObject fichaEscondida;
	int numCostado;
	GameObject padreFichas;
	GameObject padreBordes;
	List<Vector3> posicionesIniciales = new List<Vector3>();
	List<Vector3> posicionesBarajeadas = new List<Vector3>();
	GameObject[] _fichas;
	UI ui;


	void Awake()
	{
		//Recuperamos el padre de las fichas y de los bordes
		padreFichas = GameObject.Find("Fichas");
		padreBordes = GameObject.Find("Bordes");
		ui = GameObject.FindGameObjectWithTag("GameController").GetComponent<UI>();
		//tamañoInput = GameObject.Find("TamañoPuzzle").GetComponent(typeof(InputField)) as InputField;
	}

	public bool BuildGame(string imageName,ChangeDificulty.Modes mode)
    {
        //verificamos y aplicamos la dificultad
        switch (mode)
        {
			case ChangeDificulty.Modes.Easy:
				numFichasLado = 4;
				Camera.main.orthographicSize = 6;
				break;
			case ChangeDificulty.Modes.Normal:
				numFichasLado = 6;
				Camera.main.orthographicSize = 8;
				break;
			case ChangeDificulty.Modes.Hard:
				numFichasLado = 8;
				Camera.main.orthographicSize = 10;
				break;
			default:
				return false;
        }

		//verificamos si el nombre de la imagen es correcto
		foreach(Sprite image in images)
        {
            if (image.name.Equals(imageName))
            {
				RecortarImagen(image.texture);
				return true;
            }
        }
		return false;
    }

	public void RecortarImagen(Texture2D texture)
    {
		int contador = 0;
		int tamLado;
		if (texture.height > texture.width)
			tamLado = Mathf.RoundToInt(texture.width / numFichasLado);
		else
			tamLado = Mathf.RoundToInt(texture.height / numFichasLado);

		for(int y = numFichasLado - 1; y >= 0; y--)
        {
			for(int x = 0; x < numFichasLado; x++)
            {
				Sprite sprite = Sprite.Create(texture, new Rect(x * tamLado, y * tamLado, tamLado, tamLado), new Vector2(0, 0), tamLado);
				sprite.name = "F_" + contador;
				fichaImg.Add(sprite);
				contador++;
            }
        }
		fichaEscondidaImg = fichaImg[numFichasLado - 1];
		if (Mathf.Sqrt(fichaImg.Count) == Mathf.Round(Mathf.Sqrt(fichaImg.Count)))
		{
			CrearFichas();
		}
		else
			print("Imposible crear fichas");
	}

	void CrearFichas()
	{
		int contador = 0;
		numCostado = (int)Mathf.Sqrt(fichaImg.Count);
		//Doble bucle para colocar todas las fichas en su sitio
		for (int alto = numCostado + 2; alto > 0; alto--)
		{
			for (int ancho = 0; ancho < numCostado + 2; ancho++)
			{
				Vector3 posicion = new Vector3(ancho - (numCostado / 2), alto - (numCostado / 2), 0);   //posición de cada ficha

				//Comprobar si son posiciones de borde o de fichas
				if (alto == 1 || alto == numCostado + 2 || ancho == 0 || ancho == numCostado + 1)
				{   //Es parte del borde
					GameObject borde = Instantiate(bordePrfb, posicion, Quaternion.identity);       //Instanciamos el borde
					borde.transform.parent = padreBordes.transform;                             //lo ponemos cómo hijo de PadreBordes
				}
				else
				{                                                                           //Es parte del puzzle
					GameObject ficha = Instantiate(fichaPrfb, posicion, Quaternion.identity);           //Instanciamos la ficha
					ficha.GetComponent<SpriteRenderer>().sprite = fichaImg[contador];           //Asignamos el sprite a cada ficha
					ficha.transform.parent = padreFichas.transform;                             //Lo ponemos como hijo de PadreFichas
					ficha.name = fichaImg[contador].name;                                       //Dejamos el mismo nombre que el Sprite
					if (ficha.name == fichaEscondidaImg.name)
					{                               //Si es la ficha que tenemos que esconder
						fichaEscondida = ficha;                                             //La asignamos
					}
					contador++;
				}
			}
		}
		fichaEscondida.gameObject.SetActive(false);                         //Escondemos la fichaEscondida
																			//Almacenar las posiciones iniciales
		_fichas = GameObject.FindGameObjectsWithTag("Ficha");                       //Recuperamos todas las fichas con el tag ficha
		for (int i = 0; i < _fichas.Length; i++)
		{
			posicionesIniciales.Add(_fichas[i].transform.position);         //Asignamos las posiciones iniciales de las fichas
		}
		if(barajear)
			Barajar();
        else
        {
			//si no se barajea entonces las posiciones de fichas barajeadas
			//seran las mismas a las iniciales
			foreach (Vector3 pos in posicionesIniciales)
				posicionesBarajeadas.Add(pos);
        }
	}

	void Barajar()
	{
		int aleatorio;
		//Barajamos las posiciones de las fichas
		for (int i = 0; i < _fichas.Length; i++)
		{
			aleatorio = Random.Range(i, _fichas.Length);        //Creamos un numero aleatorio entre i y el numero de fichas
			Vector3 posTemp = _fichas[i].transform.position;        //En una variable temporal guardamos la posición inicial
			_fichas[i].transform.position = _fichas[aleatorio].transform.position;  //Cambiamos la posición ficha[aleatorio] por ficha[i]
			_fichas[aleatorio].transform.position = posTemp;        //asignamos la posicion inicial que habíamos guardado a fichas[aleatorio]
		}
		//guardamos las posiciones con las que comienza el juego para usarlas
		//en el boton de reiniciar
		foreach(GameObject ficha in _fichas)
			posicionesBarajeadas.Add(ficha.transform.position);
	}

	public void Reiniciar()
    {
		for(int i = 0; i < _fichas.Length; i++)
        {
			_fichas[i].transform.position = posicionesBarajeadas[i];
        }
		ui.ReiniciarMovimientos();
    }

	public void Rendirse()
    {
		GameObject[] childrens;
		//borramos los bordes
		childrens = new GameObject[padreBordes.transform.childCount];
		int i = 0;
		foreach(Transform child in padreBordes.transform)
        {
			childrens[i] = child.gameObject;
			i++;
        }
		foreach (GameObject child in childrens)
			DestroyImmediate(child.gameObject);
		//borramos las fichas
		childrens = new GameObject[padreFichas.transform.childCount];
		i = 0;
		foreach(Transform child in padreFichas.transform)
        {
			childrens[i] = child.gameObject;
			i++;
        }
		foreach (GameObject child in childrens)
			DestroyImmediate(child.gameObject);
		ui.ReiniciarMovimientos();
		fichaImg.Clear();
		posicionesIniciales.Clear();
		posicionesBarajeadas.Clear();
    }

	public void JuegarDeNuevo()
    {
		Rendirse();
		mensajeGanador.GetComponent<Animator>().SetBool("IsActive", false);
    }

	public void ComprobarGanador()
	{
		for (int i = 0; i < _fichas.Length; i++)
		{
			if (posicionesIniciales[i] != _fichas[i].transform.position)
			{   //Repasamos las posiciones actuales y sólo que
				return;                                                     //una ya no tenga la misma posición que la inicial
																			//salimos de la función
			}
		}
		fichaEscondida.gameObject.SetActive(true);  //Si hemos llegado a este punto es que hemos resuelto el puzzle
		print("Puzzle resuelto!");
		mensajeGanador.gameObject.SetActive(true);
	}
}