# PhotoStock - Refactoring do DDD

## Opis
B�dziesz tworzy� system b�d�cy cz�ci� og�lno-�wiatowego portalu (PhotoStock) do handlu zdj�ciami. 
Portal obs�uguje zar�wno artyst�w, kt�rzy mog� sprzedawa� swoje zdj�cia, jak i grafik�w kt�ry te zdj�cia kupuj�. 
W momencie kiedy kupuj�cy zaakceptuje swoja decyzje staje si� w�a�cicielem praw autorskich.

## Wymagania og�lne
System powinien pozwoli� artystom na dodawanie i zarz�dzanie zdj�ciami w katalogu poprzez stron� www.  
Katalog ten powinien by� dost�pny r�wnie� dla grafik�w kt�rzy b�d� z niego wybiera� zdj�cia. 
Sk�adane zam�wienie b�dzie r�wnie� realizowane przez stron� www. Proces kompletowania zam�wienia mo�e 
trwa� wiele dni. 
Konto klient mo�e do�adowa� swoje konto prepaid.  
P�atno�ci s� importowane z banku przez dedykowane API. 
Jest to realizowane 3 razy dziennie. Dla zam�wie� w pe�ni op�aconych system wystawia faktur� i zleca 
wysy�k� zdj�� w formie papierowej. Graficy s� informowani mailowo o zrealizowanej wysy�ce zdj��.

## Wymagania szczeg�owe ( strona kupuj�ca )
Zakres prac b�dzie dotyczy� obs�ugi zam�wie� strony kupuj�cej. Kupuj�cy b�dzie mia� mo�liwo�� wst�pnej 
rezerwacji zdj��

1. 
Klienci mog� dodawa� zdj�cia do zam�wienia z katalogu. Kilkukrotne dodanie do zam�wienia tego samego 
zdj�cia nie ma sensu. System uniemo�liwia r�wnie� kilkakrotny zakup (r�ne zam�wienia) tego samego 
zdj�cia przez jednego u�ytkownika.

2. 
Klienci mog� podda� zam�wienie zmianom zanim ostatecznie jest zatwierdz�. Zam�wienie nie jest wi���ce 
do momentu zatwierdzenia. Jest w zasadzie wst�pn� rezerwacj� produkt�w. 

W trakcie pracy nad rezerwacj� ceny zdj�� mog� ulec zmianie. Zasada jest taka, �e klient zawsze rezerwuje 
produkty po cenach aktualnie obowi�zuj�cych w systemie (a nie takich jakie widzia�), ale musi r�wnie� 
widzie� ceny jakie zostan� zastosowane. 

Klienci przed zatwierdzeniem rezerwacji musz� zobaczy� aktualny ofert� do zam�wienia jak� zaproponuje 
mu system.  Oferta powinien zawiera� produkty wraz z aktualnymi cenami (a w przysz�o�ci 
r�wnie� z aktualnie obowi�zuj�cymi promocjami).  Mo�e zaj�� sytuacja gdy dzie�o jest ju� 
niedost�pne (np. z powodu usuni�cia z katalogu). Oczywi�cie klient powinien widzie� 
taki produkt, ale jako nieaktywny.

Oferta nie jest zobowi�zuj�ca, klient mo�e zatwierdzi� lub odrzuci� tak� ofert�. Zatwierdzaj�c 
ofert� dokonujemy formalnego zakupu. Je�eli oferta, kt�r� widzia� klient jest inna ni� obowi�zuj�ca 
oferta, to nie mo�e doj�� do zakupu a klient powinien w�wczas zobaczy� now� ofert�. 

3.
Gdy klient zatwierdza zam�wienie (ofert�) to w�wczas system nie zezwala na zakupy je�eli klient nie 
posiada dostatecznej ilo�ci pieni�dzy (konto jest do�adowane w formie prepaid przez zewn�trzny system).

4. 
System powinien przechowywa� podpisan� cyfrowo histori� zakup�w klienta (co, kiedy i za ile) jako ew. 
dow�d w post�powaniu s�dowym. Dla ka�dego zakupu powinien by� generowany fakt zakupu,  kt�ry odzwierciedla 
ceny z oferty i nie podlega ju� zmianom gdy zmieniaj� si� ceny katalogowe i rabaty.

5. 
Podczas zakupu sprawdzamy czy klient posiada dostateczn� ilo�� �rodk�w (credit). Je�eli nie, to nie mo�e 
dokona� zakupu, chyba, �e jest klientem VIP, w�wczas udzielamy mu kredytu, chyba, �e przekroczy� 
limit kredyt�w. Je�eli klient posiada dostateczn� ilo�� �rodk�w (wg powy�szych regu�) to pobieramy 
op�at� i odnotowujemy fakt p�atno�ci.


