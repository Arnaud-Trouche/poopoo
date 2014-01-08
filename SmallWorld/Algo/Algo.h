#ifdef WANTDLLEXP
#define DLL _declspec(dllexport)
#define EXTERNC extern "C"
#else
#define DLL
#define EXTERNC
#endif

#define DESERT 0
#define EAU 1
#define FORET 2
#define MONTAGNE 3
#define PLAINE 4

class DLL Algo {
public:
	Algo() {}
	~Algo() {}
	int* creationCarte(int taille);
	int Algo::positionnerJoueurHorsEau(int* carte, int taille, int pos);
	int* Algo::positionnerJoueurs(int* carte, int taille);

	void Algo::traitementCaseNormale(int* carte, int pos, double* carteCoutRep, int*carteDepRes, double pointDeDeplacement);
	//Gaulois
	void Algo::deplacementPossibleGauloisInit(int* carte, int taille, int pos, double* carteCoutDep, int* carteDepRes, double ptDpl);
	void Algo::deplacementPossibleGauloisRec(int* carte, int taille, int pos, double* carteCoutDep, int* carteDepRes);
	void Algo::deplacementPossibleGauloisCase(int* carte, int taille, int pos, double* carteCoutDep, int* carteDepRes, double ptDeDepl);
	//Nain
	int* Algo::deplacementPossibleNainInit(int* carte, int taille, int pos);
	//Viking
	int* Algo::deplacementPossibleVikingInit(int* carte, int taille, int pos);

};

// A ne pas implémenter dans le .h !
EXTERNC DLL Algo* Algo_new();
EXTERNC DLL void Algo_delete(Algo* algo);
EXTERNC DLL int* Algo_creationCarte(Algo* algo, int taille);
EXTERNC DLL int Algo_positionnerJoueurHorsEau(Algo* algo, int* carte, int taille, int pos);
EXTERNC DLL int* Algo_positionnerJoueurs(Algo* algo, int* carte, int taille);

EXTERNC DLL void Algo_traitementCaseNormale(Algo* algo, int* carte, int pos, double* carteCoutRep, int*carteDepRes, double pointDeDeplacement);
//Gaulois
EXTERNC DLL void Algo_deplacementPossibleGauloisInit(Algo* algo, int* carte, int taille, int pos, double* carteCoutDep, int* carteDepRes, double ptDpl);
EXTERNC DLL void Algo_deplacementPossibleGauloisRec(Algo* algo, int* carte, int taille, int pos, double* carteCoutDep, int* carteDepRes);
EXTERNC DLL void Algo_deplacementPossibleGauloisCase(Algo* algo, int* carte, int taille, int pos, double* carteCoutDep, int* carteDepRes, double ptDeDepl);

//Nain
EXTERNC DLL int* Algo_deplacementPossibleNainInit(Algo* algo, int* carte, int taille, int pos);

//Viking
EXTERNC DLL int* Algo_deplacementPossibleVikingInit(Algo* algo, int* carte, int taille, int pos);
