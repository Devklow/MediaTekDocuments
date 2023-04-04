Feature: RechercheDeLivre

@mytag
Scenario: Afficher les livres avec le titre  
	Given Positionnement sur l onglet livre
	When Saisie de la partie du titre de livre 'les' 
	Then Le nombre de livres obtenu est de 4  
	# Liste attendue : 
	# - 00007 Dans les coulisses du musée
	# - 00019 Guide Vert - Iles Canaries 
	# - 00005 Les anonymes
	# - 00021 Les déferlantes

Scenario: Afficher le livre avec un numero
	Given Positionnement sur l onglet livre
	Given Saisie du numero de livre '00021' 
	When Clique sur le bouton recherche livre
	Then Le nombre de livres obtenu est de 1 
	# Liste attendue :
	# - 00021 Les déferlantes

Scenario: Afficher les livres pour un genre
	Given Positionnement sur l onglet livre
	When Selection du genre 2
	Then Le nombre de livres obtenu est de 5
# genre :
	# - 10001 Bande déssinée

Scenario: Afficher les livres pour un public
	Given Positionnement sur l onglet livre
	When Selection du public 1
	Then Le nombre de livres obtenu est de 12
# public :
	# - 00002 Adulte

Scenario: Afficher les livres pour un rayon
	Given Positionnement sur l onglet livre
	When Selection du rayon 3
	Then Le nombre de livres obtenu est de 1
# rayon :
	# - JN001 Jeunnesse BD
