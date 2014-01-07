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
int positionnerJoueurHorsEau(int* carte, int taille, int pos) { return algo->positionnerJoueurHorsEau(carte, taille, pos); }
int* positionnerJoueurs(int* carte, int taille) { return algo->positionnerJoueurs(carte, taille); }

void Algo_traitementCaseNormale(int* carte, int pos, double* carteCoutRep, int*carteDepRes, double pointDeDeplacement) { return algo->traitementCaseNormale(carte, pos, carteCoutRep, carteDepRes, pointDeDeplacement); }

void Algo_deplacementPossibleGauloisInit(int* carte, int taille, int pos, double* carteCoutDep, int* carteDepRes) { return algo->deplacementPossibleGauloisInit(carte, taille, pos, carteCoutDep, carteDepRes); }
void Algo_deplacementPossibleGauloisRec(int* carte, int taille, int pos, double* carteCoutDep, int* carteDepRes) { return algo->deplacementPossibleGauloisRec(carte, taille, pos, carteCoutDep, carteDepRes); }
void Algo_deplacementPossibleGauloisCase(int* carte, int taille, int pos, double* carteCoutDep, int* carteDepRes, double ptDeDepl) { return algo->deplacementPossibleGauloisCase(carte, taille, pos, carteCoutDep, carteDepRes, ptDeDepl); }

};
}
#endif