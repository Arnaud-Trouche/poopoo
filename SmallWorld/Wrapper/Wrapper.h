#ifndef __WRAPPER__
#define __WRAPPER__

#include "../Algo/Algo.h" // Changé
#pragma comment(lib, "../Debug/Algo.lib") // Changé

using namespace System;

namespace Wrapper {

public ref class WrapperAlgo {
private:
Algo* algo;
public:
WrapperAlgo(){ algo = Algo_new(); }
~WrapperAlgo(){ Algo_delete(algo); }
int* creationCarte(int taille) { return algo->creationCarte(taille); }
int positionnerJoueurHorsEau(int* carte, int taille, int pos, int dir) { return algo->positionnerJoueurHorsEau(carte, taille, pos, dir); }
int* positionnerJoueurs(int* carte, int taille) { return algo->positionnerJoueurs(carte, taille); }

//Gaulois
int* Algo_deplacementPossibleGaulois1(int* carte, int taille, int pos) { return algo->deplacementPossibleGaulois1(carte, taille, pos); }
int* Algo_deplacementPossibleGaulois2(int* carte, int taille, int pos) { return algo->deplacementPossibleGaulois2(carte, taille, pos); }
//Nain
int* Algo_deplacementPossibleNainInit(int* carte, int taille, int pos) { return algo->deplacementPossibleNainInit(carte, taille, pos); }
//Viking
int* Algo_deplacementPossibleVikingInit(int* carte, int taille, int pos) { return algo->deplacementPossibleVikingInit(carte, taille, pos); }

int* Algo_mymalloc(int taille) { return algo->mymalloc(taille); }
};
}
#endif