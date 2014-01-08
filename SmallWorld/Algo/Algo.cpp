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

int Algo::positionnerJoueurHorsEau(int* carte, int taille, int pos){
	bool trouve = false;
	while(!trouve){
		if(carte[pos] == EAU){
			pos = (pos+1)%(taille*taille);
		}else{
			trouve = true;
		}
	}
	return pos;
}

int* Algo::positionnerJoueurs(int* carte, int taille){
	int* tabPositions = (int*)malloc(2*sizeof(int));
	
	int posSouhaitee = taille - 1; //Placement souhaité en haut à droite pour le Joueur 1
	tabPositions[0] = positionnerJoueurHorsEau(carte, taille, posSouhaitee); //Placement du Joueur 1

	posSouhaitee = taille*(taille - 1); //Placement souhaité en bas à gauche pour le Joueur 2
	tabPositions[1] = positionnerJoueurHorsEau(carte, taille, posSouhaitee); //Placement du Joueur 2

	return tabPositions;
}

void Algo::deplacementPossibleGauloisInit(int* carte, int taille, int pos, double* carteCoutDep, int* carteDepRes, double ptDpl){
	int i;
	//Initialise la Carte Resultat des déplacement possible avec la valeur NonTraitee
	for (i = 0; i < (taille*taille); i++)
		carteDepRes[i] = CASE_NONTRAITEE;

	//Initialise la Carte des Cout des déplacements
	for (i = 0; i < (taille*taille); i++)
		carteCoutDep[i] = 0;

	//On autorise la case initiale pour démarrer l'algo de recherche
	carteCoutDep[pos] = 1;

	//Analyse la case actuelle car elle ne sera pas analysée par la suite. Si c'est une case Plaine -> la mettre comme Bonus
	if (carte[pos] == CASE_PLAINE)
	{
		carteDepRes[pos] = CASE_BONUS;
	}
	else
	{
		carteDepRes[pos] = CASE_POSSIBLE;
	}

	carteCoutDep[pos] = ptDpl;

	deplacementPossibleGauloisRec(carte, taille, pos, carteCoutDep, carteDepRes);
}

void Algo::deplacementPossibleGauloisRec(int* carte, int taille, int pos, double* carteCoutDep, int* carteDepRes){

	int posTemp = pos;
	
	//Si on est pas en haut
	if (pos > taille - 1){
		posTemp = pos - taille;
		if (carteDepRes[posTemp] == CASE_NONTRAITEE) {
			//On regarde la case du haut
			deplacementPossibleGauloisCase(carte, taille, posTemp, carteCoutDep, carteDepRes, carteDepRes[pos]);
			//Appel Recursif
			deplacementPossibleGauloisRec(carte, taille, posTemp, carteCoutDep, carteDepRes);
		}
	}

	//Si on est pas en bas
	if (pos < taille*(taille - 1) ) {
		posTemp = pos + taille;
		if (carteDepRes[posTemp] == CASE_NONTRAITEE) {
			//On regarde la case de bas
			deplacementPossibleGauloisCase(carte, taille, posTemp, carteCoutDep, carteDepRes, carteDepRes[pos]);
			//Appel Recursif
			deplacementPossibleGauloisRec(carte, taille, posTemp, carteCoutDep, carteDepRes);
		}
	}

	//Si on est pas a gauche
	if (pos != 0 || (pos % taille) != 0) { 
		posTemp = pos - 1;
		if (carteDepRes[posTemp] == CASE_NONTRAITEE) {
			//On regarde la case a gauche
			deplacementPossibleGauloisCase(carte, taille, posTemp, carteCoutDep, carteDepRes, carteDepRes[pos]);
			//Appel Recursif
			deplacementPossibleGauloisRec(carte, taille, posTemp, carteCoutDep, carteDepRes);
		}
	}

	//Si on est pas a droite
	if (pos%(taille-1) != 0) {
		posTemp = pos + 1;
		if (carteDepRes[posTemp] == CASE_NONTRAITEE) {
			//On regarde la case du haut
			deplacementPossibleGauloisCase(carte, taille, posTemp, carteCoutDep, carteDepRes, carteDepRes[pos]);
			//Appel Recursif
			deplacementPossibleGauloisRec(carte, taille, posTemp, carteCoutDep, carteDepRes);
		}
	}


}

void Algo::traitementCaseNormale(int* carte, int pos, double* carteCoutDep, int* carteDepRes, double PointDeDeplacement){
	
	if (PointDeDeplacement >= 1)
	{
		PointDeDeplacement--;
		carteDepRes[pos] = CASE_POSSIBLE;
		carteCoutDep[pos] = PointDeDeplacement;
	}
	else
	{
		carteDepRes[pos] = CASE_IMPOSSIBLE;
	}

}

void Algo::deplacementPossibleGauloisCase(int* carte, int taille, int pos, double* carteCoutDep, int* carteDepRes, double ptDeDepl){

	double PointDeDeplacement = ptDeDepl;

	//Comportement varie selon le type de la case
	switch (carte[pos]) {

	case CASE_DESERT:
		traitementCaseNormale(carte, pos, carteCoutDep, carteDepRes, PointDeDeplacement);
		break;

	case CASE_MONTAGNE:
		traitementCaseNormale(carte, pos, carteCoutDep, carteDepRes, PointDeDeplacement);
		break;


	case CASE_PLAINE:
		if (PointDeDeplacement >= 0.5)
		{
			PointDeDeplacement -= 0.5;
			carteDepRes[pos] = CASE_BONUS;
			carteCoutDep[pos] = PointDeDeplacement;
		}
		else
		{
			carteDepRes[pos] = CASE_IMPOSSIBLE;
		}
		break;

	default:
		break;

	}

}


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
	if (pos != 0 || (pos % taille) != 0) {
		if (carte[pos - 1] != CASE_EAU)
			carteDepRes[pos - 1] = CASE_POSSIBLE;
	}

	//Si on est pas a droite
	if (pos%(taille - 1) != 0) {
		if (carte[pos + 1] != CASE_EAU)
			carteDepRes[pos + 1] = CASE_POSSIBLE;
	}

	return carteDepRes;
}

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
	if (pos != 0 || (pos % taille) != 0) {
		carteDepRes[pos - 1] = CASE_POSSIBLE;
	}

	//Si on est pas a droite
	if (pos%(taille - 1) != 0) {
		carteDepRes[pos + 1] = CASE_POSSIBLE;
	}

	return carteDepRes;
}


Algo* Algo_new() { return new Algo(); }
void Algo_delete(Algo* algo) { delete algo; }
int* Algo_creationCarte(Algo* algo, int taille) { return algo->creationCarte(taille); } 

int Algo_positionnerJoueurHorsEau(Algo* algo, int* carte, int taille, int pos) { return algo->positionnerJoueurHorsEau(carte, taille, pos); }
int* Algo_positionnerJoueurs(Algo* algo, int* carte, int taille) { return algo->positionnerJoueurs(carte, taille); }

void Algo_traitementCaseNormale(Algo* algo, int* carte, int pos, double* carteCoutDep, int* carteDepRes, double PointDeDeplacement) { return algo->traitementCaseNormale(carte, pos, carteCoutDep, carteDepRes, PointDeDeplacement); }

void Algo_deplacementPossibleGauloisInit(Algo* algo, int* carte, int taille, int pos, double* carteCoutDep, int* carteDepRes, double ptDpl) { return algo->deplacementPossibleGauloisInit(carte, taille, pos, carteCoutDep, carteDepRes,ptDpl); }
void Algo_deplacementPossibleGauloisRec(Algo* algo, int* carte, int taille, int pos, double* carteCoutDep, int* carteDepRes) { return algo->deplacementPossibleGauloisRec(carte, taille, pos, carteCoutDep, carteDepRes); }
void Algo_deplacementPossibleGauloisCase(Algo* algo, int* carte, int taille, int pos, double* carteCoutDep, int* carteDepRes, double ptDeDepl) { return algo->deplacementPossibleGauloisCase(carte, taille, pos, carteCoutDep, carteDepRes, ptDeDepl); }

int* Algo_deplacementPossibleNainInit(Algo* algo, int* carte, int taille, int pos) { return algo->deplacementPossibleNainInit(carte, taille, pos); }

int* Algo_deplacementPossibleVikingInit(Algo* algo, int* carte, int taille, int pos) { return algo->deplacementPossibleVikingInit(carte, taille, pos); }


//void Algo_deplacementPossibleGauloisVerifCase
