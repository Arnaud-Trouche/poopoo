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
};

// A ne pas implémenter dans le .h !
EXTERNC DLL Algo* Algo_new();
EXTERNC DLL void Algo_delete(Algo* algo);
EXTERNC DLL int* Algo_creationCarte(Algo* algo, int taille);
EXTERNC DLL int Algo_positionnerJoueurHorsEau(Algo* algo, int* carte, int taille, int pos);
EXTERNC DLL int* Algo_positionnerJoueurs(Algo* algo, int* carte, int taille);