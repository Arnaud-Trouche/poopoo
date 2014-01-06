#include "Algo.h"
#include <stdio.h>
#include <stdlib.h>
#include <time.h>



#define CASE_DESERT 0
#define CASE_EAU 1
#define CASE_FORET 2
#define CASE_MONTAGNE 3
#define CASE_PLAINE 4

#define CASE_NONCALCULEE 0
#define CASE_IMPOSSIBLE 1
#define CASE_POSSIBLE 2
#define CASE_OPTIMALE 3

int* Algo::creationCarte(int taille) {
	srand(time(NULL));
	int nbcases = taille*taille/5;
	int types[5] = {nbcases,nbcases,nbcases,nbcases,nbcases};
	int* cases = (int*)malloc(taille*taille);
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



Algo* Algo_new() { return new Algo(); }
void Algo_delete(Algo* algo) { delete algo; }
int* Algo_creationCarte(Algo* algo, int taille) { return algo->creationCarte(taille); } 
int Algo_positionnerJoueurHorsEau(Algo* algo, int* carte, int taille, int pos) { return algo->positionnerJoueurHorsEau(carte, taille, pos); }
int* Algo_positionnerJoueurs(Algo* algo, int* carte, int taille) { return algo->positionnerJoueurs(carte, taille); }

