# IndividualneZadanie1

Program: AutobazarApp,
Autor: Barbora Kubišová  kubisova@kros.sk,
Popis: Program AutobazarApp slúži na spravovanie áut v autobazáre.

Dáta sa ukladajú do súboru autobazar.txt do adresára, v ktorom bol program spustený.
Program predpokladá, že má práva na prácu  s týmto súborom.
Ak nastane chyba pri práci so súborom, chyba sa zapíše do súboru errorLog.txt

Autobazar.cs - hlavná trieda (statická),
VehicleNotFoundException - custom exception,
AutobazarMenu - menu,
Vehicle - trieda s properties pre auto,
Helpers\InputValidator - získanie a validácia vstupov,
Helpers\ErrorLogger - zápis chýb pri práci so súborom,
Helpers\ConsoleWriter - vypísanie horizontálnej čiary na konzolu.
