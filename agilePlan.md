# Stellar Snack

## Koncept
Hra je převážně inspirována flash hrou "Tasty Planet" a herní sérií "Katamari Damacy".
Hlavní postava bude jíst menší objekty/nepřátele, a tím se bude zvětšovat. 
Interakcí s většími nepřáteli hlavní postava může být zmenšena/snězena.
Po dosáhnutí dostatečné velikosti je změněno měřítko hry. Například z pojídání buněk se přejde na pojídání malých ryb.
Hra má časový limit. Při změně měřítka hráč dostane čas navíc. Cílem je za přidělený čas dosáhnout co největší velikosti. Pokud je hlavní postava snězena, hra automaticky končí. Výsledné skóre je nejvyšší dosažené skóre.
Hra bude mít několik prostředí vázaných na různá měřítka.

## Předpokládaný postup vývoje hry
1. Vytvoření postavy, která se pohybuje dle vstupů uživatele.
	- Postava byla vytvořena jako CharacterBody2D se Sprite2D a CollisionShape2D
	- Byly vytvořeny vstupy pro čtyři směry (nahoru, dolu, doleva, doprava)
	- Vstupy byly detekovány ve skriptu hlavní postavy
	- Podle vstupů se mění movement vector postavy
	- Pro postavu byl vytvořený sprite
2. Mechanika růstu/zmenšování.
	- Byla vytořena funkce pro změnu velikosti hlavní postavy
		- Funkce mění velikost celého CharacterBody2D => mění se jak sprite tak kolize
3. Přidání objektů, které lze konzumovat.
	- Byla vytvořena třída Edible, která slouží jako základ tříd pro nepřátele pomocí dědičnosti
	- Třída edible má bool "hostile", který udává, zda neúspěšná konzumace hráče zraní, nebo ne
	- Třída edible má velikost ktrá určuje, zda objekt hráč může sníst
4. Vytvoření komplexnějších nepřátel s pathfindingem.
	- Bylo vytvořeno několik typů nepřátel/jídla
	- První typ jídla se nepohybuje.
	- Druhý typ jídla následuje hráče, dokud může hráče zranit. Pokud hráč dosáhne dostatečné velikosti, nepřítel se pokouší uniknout.
	- Třetí typ jíla následuje hráče, ale snaží se držet odstup. Periodicky po hráči střílí rakety. Pokud rakety hráče trefí, ubírají mu život.
5. Přidání různých měřítek a přechody mezi měřítky.
	- Byly vytvořeny čtyři levely, které reprezentují růst hráče.
	- Každý level má vlastní druhy jídla s různými texturami.
	- Každý level má vlastní hudbu.
	- Po splnění cíle levelu se přechází do dalšího levelu
	- Čtvrtý level nemá konec => hráč se musí snažit získat co nejvyšší skóre.
6. Časový limit.
	- Hráč má časový limit, po kterém hra končí
	- Po přechodu do nového levelu hráč dostane čas navíc
	- Po vypršení času hráč prohrává
7. Vytvoření základního menu
	- Bylo přidání hlavní menu se jménem hry, možností hru spustit a možností hru ukončit
	- Bylo přidáno game over menu s nahraným skóre hráče, možností jít do hlavního menu a možností ukončit hru
