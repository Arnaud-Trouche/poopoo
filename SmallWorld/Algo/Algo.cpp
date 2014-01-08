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
		if (carte[pos - 1] != CASE_PLAINE)
			carteDepRes[pos - 1] = CASE_POSSIBLE;
	}

	//Si on est pas a droite
	if (((pos + 1)%taille) != 0) {
		if (carte[pos + 1] != CASE_PLAINE)
			carteDepRes[pos + 1] = CASE_POSSIBLE;
	}

	return carteDepRes;
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


Algo* Algo_new() { return new Algo(); }
void Algo_delete(Algo* algo) { delete algo; }
int* Algo_creationCarte(Algo* algo, int taille) { return algo->creationCarte(taille); } 

int Algo_positionnerJoueurHorsEau(Algo* algo, int* carte, int taille, int pos) { return algo->positionnerJoueurHorsEau(carte, taille, pos); }
int* Algo_positionnerJoueurs(Algo* algo, int* carte, int taille) { return algo->positionnerJoueurs(carte, taille); }

int* Algo_deplacementPossibleGaulois1(Algo* algo, int* carte, int taille, int pos) { return algo->deplacementPossibleGaulois1(carte, taille, pos); }
int* Algo_deplacementPossibleGaulois2(Algo* algo, int* carte, int taille, int pos) { return algo->deplacementPossibleGaulois2(carte, taille, pos); }

int* Algo_deplacementPossibleNainInit(Algo* algo, int* carte, int taille, int pos) { return algo->deplacementPossibleNainInit(carte, taille, pos); }

int* Algo_deplacementPossibleVikingInit(Algo* algo, int* carte, int taille, int pos) { return algo->deplacementPossibleVikingInit(carte, taille, pos); }


//void Algo_deplacementPossibleGauloisVerifCase
