#include "Algo.h"
#include <stdio.h>
#include <stdlib.h>
#include <time.h>

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

int Algo::positionnerJoueur(int* carte, int taille, int pos){
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


Algo* Algo_new() { return new Algo(); }
void Algo_delete(Algo* algo) { delete algo; }
int* Algo_creationCarte(Algo* algo, int taille) { return algo->creationCarte(taille); } 
int Algo_positionnerJoueur(Algo* algo, int* carte, int taille, int pos) { return algo->positionnerJoueur(carte, taille, pos); }