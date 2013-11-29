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
int positionnerJoueur(int* carte, int taille, int pos) { return algo->positionnerJoueur(carte, taille, pos); }
};
}
#endif