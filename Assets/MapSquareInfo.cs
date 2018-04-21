﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSquareInfo : MonoBehaviour
{
    public int cooX;
    public int cooY;
    public int state;
    public int probforplateau=10;

    public string coo;
 
    public bool CurrentPosition = false;
    public bool isExplored = false;
    public bool HasPlateau = false;

    public Plateau myPlateau;

    public Canvas Map;
    
    
    public Material ExploredMat;
    public Material UnexploredMat;
    public Material HasPlateauMat;

    public Color Red;
    public Color Green;
    public Color Blue;

    Exploration E;
    MapGenerator GM;
	void Start ()
    {
        GM = FindObjectOfType<MapGenerator>();
        E = FindObjectOfType<Exploration>();
        if(E.CooToPlateau.ContainsKey(GM.makeCoo(cooX,cooY)))
        {
            
            myPlateau = E.CooToPlateau[GM.makeCoo(cooX,cooY)];
        }
        

        this.transform.localPosition = new Vector3(cooX*51,cooY*51,0);
        this.GetComponentInChildren<Text>().text = GM.makeCoo(cooX, cooY);
        RollForPlateau();
        definemycolor();
        
	}
    public void ShowPlateau()
    {
        if (E.CooToPlateau.ContainsKey(GM.makeCoo(cooX, cooY)))
        {
            
            myPlateau = E.CooToPlateau[GM.makeCoo(cooX, cooY)];
        }
        E.InfoIleText.text = "";
        
        if (myPlateau!=null)
        {
            GM.IsDiscoverd = true;
            E.InfoIleText.text = coo + "\n" + "Info: " + "\n" + "coordones: "+myPlateau.Coo + "\n" + "Taille: "+myPlateau.Size + "\n" + "Type: "+myPlateau.TypeName + "\n" + "Montagnes: " + "\n" + "Ressources: "+myPlateau.ChestProb + "\n" + "Danger: "+myPlateau.Danger;
            E.ExploreButton.GetComponent<Image>().material.color = Blue;
        }
        if(myPlateau==null)
        {
            if(state==2)
            {
                GM.IsDiscoverd = false;
                E.InfoIleText.text = coo + "\n" + "Info:" + "\n" + "Plateau Innexplore";
                E.ExploreButton.enabled = true;
                E.ExploreButton.GetComponent<Image>().material.color = Green;
            }
            if(state==1)
            {
                GM.IsDiscoverd = false;
                E.InfoIleText.text = coo + "\n" + "Info:" + "\n" + "Que de l'eau, aucun plateau par ici";
                E.ExploreButton.enabled = false;
                E.ExploreButton.GetComponent<Image>().material.color=Red;
                E.ActionText.text = "En recherche de plateau sous-marin";
            }
            if (state == 0)
            {
                GM.IsDiscoverd = false;
                E.InfoIleText.text = coo + "\n" + "Info:" + "\n" + "Cette partie de l'ocean est innexplore";
                E.ExploreButton.enabled = false;
                E.ExploreButton.GetComponent<Image>().material.color = Red;
                E.ActionText.text = "En recherche de plateau sous-marin";
            }
        }
        if(state==2)
        {
            E.ActionText.text = "Plateau sous-marin Trouve";
        }
    }
    
	public void movingToHere()
    {
        E.DestinationLongitude = cooX;
        E.DestinationLatitude = cooY;

    }
    
	public void definemycolor()
    {
        if(state==0)
        {
            this.GetComponent<Image>().material = UnexploredMat;
        }
        if (state==1)
        {
            this.GetComponent<Image>().material = ExploredMat;
        }
        if (state==2)
        {
            this.GetComponent<Image>().material = HasPlateauMat;
        }
    }
    public void RollForPlateau()
    {
        if(state==1)
        {
            int roll = Random.Range(0, 100);
            if(roll<=probforplateau)
            {
                state = 2;
                
            }

        }
    }
	void Update ()
    {
		
	}
}
