/**
* @file Algo.h
* @brief Fichier d'entête pour les algos
*
* @author <a href="mailto:samuel.coz@insa-rennes.fr">Samuel COZ</a>
* @author <a href="mailto:arnaud.trouche@insa-rennes.fr">Arnaud Trouche</a>
*
* @date 15/01/2014
* @version 0.1
*/

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

	/**
	* @fn creationCarte(int taille)
	* @brief Créer la carte de facon aléatoire avec la bonne taille
	*
	* @param int <b>taille</b> la taille de la carte
	* @return int *, un tableau contenant les types de cases créées
	*/
	int* creationCarte(int taille);

	/**
	* @fn positionnerJoueurHorsEau(int* carte, int taille, int pos, int dir)
	* @brief Renvoie la position du peuple au début de la partie en fonction d'une position de base. Il faut que le peuple ne soit pas sur une case Eau
	*
	* @param int* <b>carte</b> La carte du jeu
	* @param int <b>taille</b> La taille de la carte
	* @param int <b>pos</b> La position de base souhaitée
	* @param int <b>dir</b> La direction désirée lorsque qu'il ya une case Eau
	* @return int , La position finale du peuple en début de partie
	*/
	int Algo::positionnerJoueurHorsEau(int* carte, int taille, int pos, int dir);

	/**
	* @fn positionnerJoueurs(int* carte, int taille)
	* @brief Renvoie la position des deux peuples au début de la partie en fonction d'une position de base. Il faut que le peuple ne soit pas sur une case Eau
	*
	* @param int* <b>carte</b> La carte du jeu
	* @param int <b>taille</b> La taille de la carte
	* @return int* , Le tableau de la position finale des deux peuples en début de partie
	*/
	int* Algo::positionnerJoueurs(int* carte, int taille);

	/**
	* @fn deplacementPossibleGaulois1(int* carte, int taille, int pos)
	* @brief Renvoie le tableau des cases où une unités gauloises peut se déplacer. Elle concerne le premier déplacement d'une unité gauloise
	* @param int* <b>carte</b> La carte du jeu
	* @param int <b>taille</b> La taille de la carte
	* @param int <b>pos</b> La position de l'unité question
	* @return int* , Le tableau des positions où l'unité gauloise peut se déplacer.
	*/
	int* Algo::deplacementPossibleGaulois1(int* carte, int taille, int pos);
	
	/**
	* @fn deplacementPossibleGaulois2(int* carte, int taille, int pos)
	* @brief Renvoie le tableau des cases où une unités gauloises peut se déplacer. Elle concerne le second déplacement d'une unité gauloise
	* @param int* <b>carte</b> La carte du jeu
	* @param int <b>taille</b> La taille de la carte
	* @param int <b>pos</b> La position de l'unité question
	* @return int* , Le tableau des positions où l'unité gauloise peut se déplacer.
	*/
	int* Algo::deplacementPossibleGaulois2(int* carte, int taille, int pos);
	
	/**
	* @fn deplacementPossibleNainInit(int* carte, int taille, int pos)
	* @brief Renvoie le tableau des cases où une unité naine peut se déplacer.
	* @param int* <b>carte</b> La carte du jeu
	* @param int <b>taille</b> La taille de la carte
	* @param int <b>pos</b> La position de l'unité question
	* @return int* , Le tableau des positions où l'unité gauloise peut se déplacer.
	*/
	int* Algo::deplacementPossibleNainInit(int* carte, int taille, int pos);
	
	/**
	* @fn deplacementPossibleVikingInit(int* carte, int taille, int pos)
	* @brief Renvoie le tableau des cases où une unité viking peut se déplacer.
	* @param int* <b>carte</b> La carte du jeu
	* @param int <b>taille</b> La taille de la carte
	* @param int <b>pos</b> La position de l'unité question
	* @return int* , Le tableau des positions où l'unité gauloise peut se déplacer.
	*/
	int* Algo::deplacementPossibleVikingInit(int* carte, int taille, int pos);

	/**
	* @fn mymalloc(int taille)
	* @brief Renvoie une zone d'allocation dynamique de taille*taille*sizeof(int)
	* @param int <b>taille</b> La taille de la carte
	* @return int* , La zone d'allocation dynamique.
	*/
	int* mymalloc(int taille);
};

// A ne pas implémenter dans le .h !
EXTERNC DLL Algo* Algo_new();
EXTERNC DLL void Algo_delete(Algo* algo);
EXTERNC DLL int* Algo_creationCarte(Algo* algo, int taille);
EXTERNC DLL int Algo_positionnerJoueurHorsEau(Algo* algo, int* carte, int taille, int pos, int dir);
EXTERNC DLL int* Algo_positionnerJoueurs(Algo* algo, int* carte, int taille);

//Gaulois
EXTERNC DLL int* Algo_deplacementPossibleGaulois1(Algo* algo, int* carte, int taille, int pos);
EXTERNC DLL int* Algo_deplacementPossibleGaulois2(Algo* algo, int* carte, int taille, int pos);

//Nain
EXTERNC DLL int* Algo_deplacementPossibleNainInit(Algo* algo, int* carte, int taille, int pos);

//Viking
EXTERNC DLL int* Algo_deplacementPossibleVikingInit(Algo* algo, int* carte, int taille, int pos);

EXTERNC DLL int* Algo_mymalloc(Algo* algo, int taille);