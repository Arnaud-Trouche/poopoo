#include "Algo.h"
#include <stdio.h>
#include <stdlib.h>
#include <time.h>

int* Algo::creationCarte(int taille) {
	srand(NULL);
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

Algo* Algo_new() { return new Algo(); }
void Algo_delete(Algo* algo) { delete algo; }
int* Algo_computeAlgo(Algo* algo, int taille) { return algo->creationCarte(taille); } 