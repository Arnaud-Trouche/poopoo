/**
* @file Algo.h
* @brief Fichier source pour les algos
*
* @author <a href="mailto:samuel.coz@insa-rennes.fr">Samuel COZ</a>
* @author <a href="mailto:arnaud.trouche@insa-rennes.fr">Arnaud Trouche</a>
*
* @date 15/01/2014
* @version 0.1
*/

#include "Algo.h"
#include <stdio.h>
#include <stdlib.h>
#include <time.h>


//Constantes des types de cases
#define CASE_DESERT 0
#define CASE_EAU 1
#define CASE_FORET 2
#define CASE_MONTAGNE 3
#define CASE_PLAINE 4

//Constantes pour remplir la carte des Points de Deplacement
#define CASE_NONTRAITEE 0
#define CASE_POSSIBLE 1
#define CASE_BONUS 2
#define CASE_IMPOSSIBLE 3
#define CASE_NEUTRE 4 //Case ne rapportant pas de point 

/**
* @fn creationCarte(int taille)
* @brief Créer la carte de facon aléatoire avec la bonne taille
*
* @param int <b>taille</b> la taille de la carte
* @return int *, un tableau contenant les types de cases créées
*/
int* Algo::creationCarte(int taille) {
	srand((unsigned int)time(NULL));
	int nbcases = (taille*taille)/5;
	int types[5] = {nbcases,nbcases,nbcases,nbcases,nbcases};
	int* cases = (int*)malloc((taille*taille) * sizeof(int));
	int i, rdm;
	bool trouveType;

	for(i=0; i<taille*taille; i++){
		trouveType=false;
		while(!trouveType){
			rdm = rand()%5;
			if(types[rdm] > 0){
				cases[i]=rdm;
				types[rdm]--;
				trouveType = true;
			}
		}
	}

	return cases;
}

/**
* @fn positionnerJoueurHorsEau(int* carte, int taille, int pos, int dir)
* @brief Renvoie la position du peuple au début de la partie en fonction d'une position de base. Il faut que le peuple ne soit pas sur une case Eau
*
* @param int* <b>carte</b> La carte du jeu
* @param int <b>taille</b> La taille de la carte
* @param int <b>pos</b> La position de base souhaitée
* @param int <b>dir</b> La direction désirée lorsque qu'il ya une case Eau
* @return int , La position finale du peuple en début de partie
*/
int Algo::positionnerJoueurHorsEau(int* carte, int taille, int pos, int dir){
	bool trouve = false;
	int position = pos;
	while(!trouve){
		if(carte[position] == EAU){
			position = (position+dir);
			//Si on a une ligne d'eau en haut ou en bas de la carte 
			//il faut descendre ou monter la ligne de positionnement sinon en dehors de la carte
			if ((position > ((taille * taille) - 1)) || (position < 0)) {
				//Changement du sens de direction
				dir = -dir;
				//Changement de ligne
				position = (position + taille * (dir)) + (dir);
			}

		}else{
			trouve = true;
		}
	}
	return position;
}

/**
* @fn positionnerJoueurs(int* carte, int taille)
* @brief Renvoie la position des deux peuples au début de la partie en fonction d'une position de base. Il faut que le peuple ne soit pas sur une case Eau
*
* @param int* <b>carte</b> La carte du jeu
* @param int <b>taille</b> La taille de la carte
* @return int* , Le tableau de la position finale des deux peuples en début de partie
*/
int* Algo::positionnerJoueurs(int* carte, int taille){
	int* tabPositions = (int*)malloc(2*sizeof(int));
	
	int posSouhaitee = taille - 1; //Placement souhaité en haut à droite pour le Joueur 1
	tabPositions[0] = positionnerJoueurHorsEau(carte, taille, posSouhaitee, -1); //Placement du Joueur 1

	posSouhaitee = taille*(taille - 1); //Placement souhaité en bas à gauche pour le Joueur 2
	tabPositions[1] = positionnerJoueurHorsEau(carte, taille, posSouhaitee, 1); //Placement du Joueur 2

	return tabPositions;
}

/**
* @fn deplacementPossibleGaulois1(int* carte, int taille, int pos)
* @brief Renvoie le tableau des cases où une unités gauloises peut se déplacer. Elle concerne le premier déplacement d'une unité gauloise
* @param int* <b>carte</b> La carte du jeu
* @param int <b>taille</b> La taille de la carte
* @param int <b>pos</b> La position de l'unité question
* @return int* , Le tableau des positions où l'unité gauloise peut se déplacer.
*/
int* Algo::deplacementPossibleGaulois1(int* carte, int taille, int pos){
	int i;
	int* carteDepRes = (int*)malloc(taille*taille*sizeof(int));

	//Initialise la Carte Resultat des déplacement possible avec la valeur NonTraitee
	for (i = 0; i < taille*taille; i++)
		carteDepRes[i] = CASE_IMPOSSIBLE;

	//Si on est pas en haut
	if (pos > taille - 1){
		if (carte[pos - taille] != CASE_EAU)
			carteDepRes[pos - taille] = CASE_POSSIBLE;
	}

	//Si on est pas en bas
	if (pos < taille*(taille - 1)) {
		if (carte[pos + taille] != CASE_EAU)
			carteDepRes[pos + taille] = CASE_POSSIBLE;
	}

	//Si on est pas a gauche
	if ((pos % taille) != 0) {
		if (carte[pos - 1] != CASE_EAU)
			carteDepRes[pos - 1] = CASE_POSSIBLE;
	}

	//Si on est pas a droite
	if (((pos + 1)%taille) != 0) {
		if (carte[pos + 1] != CASE_EAU)
			carteDepRes[pos + 1] = CASE_POSSIBLE;
	}

	return carteDepRes;
}

/**
* @fn deplacementPossibleGaulois2(int* carte, int taille, int pos)
* @brief Renvoie le tableau des cases où une unités gauloises peut se déplacer. Elle concerne le second déplacement d'une unité gauloise
* @param int* <b>carte</b> La carte du jeu
* @param int <b>taille</b> La taille de la carte
* @param int <b>pos</b> La position de l'unité question
* @return int* , Le tableau des positions où l'unité gauloise peut se déplacer.
*/
int* Algo::deplacementPossibleGaulois2(int* carte, int taille, int pos){
	int i;
	int* carteDepRes = (int*)malloc(taille*taille*sizeof(int));

	//Initialise la Carte Resultat des déplacement possible avec la valeur NonTraitee
	for (i = 0; i < taille*taille; i++)
		carteDepRes[i] = CASE_IMPOSSIBLE;

	//Si on est pas en haut
	if (pos > taille - 1){
		if (carte[pos - taille] == CASE_PLAINE)
			carteDepRes[pos - taille] = CASE_POSSIBLE;
	}

	//Si on est pas en bas
	if (pos < taille*(taille - 1)) {
		if (carte[pos + taille] == CASE_PLAINE)
			carteDepRes[pos + taille] = CASE_POSSIBLE;
	}

	//Si on est pas a gauche
	if ((pos % taille) != 0) {
		if (carte[pos - 1] == CASE_PLAINE)
			carteDepRes[pos - 1] = CASE_POSSIBLE;
	}

	//Si on est pas a droite
	if (((pos + 1)%taille) != 0) {
		if (carte[pos + 1] == CASE_PLAINE)
			carteDepRes[pos + 1] = CASE_POSSIBLE;
	}

	return carteDepRes;
}

/**
* @fn deplacementPossibleNainInit(int* carte, int taille, int pos)
* @brief Renvoie le tableau des cases où une unité naine peut se déplacer.
* @param int* <b>carte</b> La carte du jeu
* @param int <b>taille</b> La taille de la carte
* @param int <b>pos</b> La position de l'unité question
* @return int* , Le tableau des positions où l'unité gauloise peut se déplacer.
*/
int* Algo::deplacementPossibleNainInit(int* carte, int taille, int pos){
	int i;
	int* carteDepRes = (int*)malloc(taille*taille*sizeof(int));
	
	//Initialise la Carte Resultat des déplacement possible avec la valeur NonTraitee
	for (i = 0; i < taille*taille; i++)
		carteDepRes[i] = CASE_IMPOSSIBLE;

	//Si sur Montagne on autorise le deplacement sur une autre montagne
	if (carte[pos] == CASE_MONTAGNE)
	{
		for (i = 0; i < taille*taille; i++)
			if (carte[i] == CASE_MONTAGNE)
				carteDepRes[i] = CASE_POSSIBLE;
	}

	//Si on est pas en haut
	if (pos > taille - 1){
		if (carte[pos - taille] != CASE_EAU)
			carteDepRes[pos - taille] = CASE_POSSIBLE;
	}

	//Si on est pas en bas
	if (pos < taille*(taille - 1)) {
		if (carte[pos + taille] != CASE_EAU)
			carteDepRes[pos + taille] = CASE_POSSIBLE;
	}

	//Si on est pas a gauche
	if ((pos % taille) != 0) {
		if (carte[pos - 1] != CASE_EAU)
			carteDepRes[pos - 1] = CASE_POSSIBLE;
	}

	//Si on est pas a droite
	if (((pos+1)%taille) != 0) {
		if (carte[pos + 1] != CASE_EAU)
			carteDepRes[pos + 1] = CASE_POSSIBLE;
	}

	return carteDepRes;
}

/**
* @fn deplacementPossibleVikingInit(int* carte, int taille, int pos)
* @brief Renvoie le tableau des cases où une unité viking peut se déplacer.
* @param int* <b>carte</b> La carte du jeu
* @param int <b>taille</b> La taille de la carte
* @param int <b>pos</b> La position de l'unité question
* @return int* , Le tableau des positions où l'unité gauloise peut se déplacer.
*/
int* Algo::deplacementPossibleVikingInit(int* carte, int taille, int pos){
	int i;

	int* carteDepRes = (int*)malloc(taille*taille*sizeof(int));

	//Initialise la Carte Resultat des déplacement possible avec la valeur NonTraitee
	for (i = 0; i < taille*taille; i++)
		carteDepRes[i] = CASE_IMPOSSIBLE;

	//Si on est pas en haut
	if (pos > taille - 1){
		carteDepRes[pos - taille] = CASE_POSSIBLE;
	}

	//Si on est pas en bas
	if (pos < taille*(taille - 1)) {
		carteDepRes[pos + taille] = CASE_POSSIBLE;
	}

	//Si on est pas a gauche
	if ((pos % taille) != 0) {
		carteDepRes[pos - 1] = CASE_POSSIBLE;
	}

	//Si on est pas a droite
	if (((pos+1) % taille) != 0) {
		carteDepRes[pos + 1] = CASE_POSSIBLE;
	}

	return carteDepRes;
}

/**
* @fn mymalloc(int taille)
* @brief Renvoie une zone d'allocation dynamique de taille*taille*sizeof(int)
* @param int <b>taille</b> La taille de la carte
* @return int* , La zone d'allocation dynamique.
*/
int* Algo::mymalloc(int taille){
	return (int*)malloc(taille*taille*sizeof(int));
}

Algo* Algo_new() { return new Algo(); }
void Algo_delete(Algo* algo) { delete algo; }
int* Algo_creationCarte(Algo* algo, int taille) { return algo->creationCarte(taille); } 

int Algo_positionnerJoueurHorsEau(Algo* algo, int* carte, int taille, int pos, int dir) { return algo->positionnerJoueurHorsEau(carte, taille, pos, dir); }
int* Algo_positionnerJoueurs(Algo* algo, int* carte, int taille) { return algo->positionnerJoueurs(carte, taille); }

int* Algo_deplacementPossibleGaulois1(Algo* algo, int* carte, int taille, int pos) { return algo->deplacementPossibleGaulois1(carte, taille, pos); }
int* Algo_deplacementPossibleGaulois2(Algo* algo, int* carte, int taille, int pos) { return algo->deplacementPossibleGaulois2(carte, taille, pos); }

int* Algo_deplacementPossibleNainInit(Algo* algo, int* carte, int taille, int pos) { return algo->deplacementPossibleNainInit(carte, taille, pos); }

int* Algo_deplacementPossibleVikingInit(Algo* algo, int* carte, int taille, int pos) { return algo->deplacementPossibleVikingInit(carte, taille, pos); }

int* Algo_mymalloc(Algo* algo,  int taille) { return algo->mymalloc(taille); }

