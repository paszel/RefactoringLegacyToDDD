# PhotoStock - Refactoring do DDD

## Opis
Bêdziesz tworzy³ system bêd¹cy czêœci¹ ogólno-œwiatowego portalu (PhotoStock) do handlu zdjêciami. 
Portal obs³uguje zarówno artystów, którzy mog¹ sprzedawaæ swoje zdjêcia, jak i grafików który te zdjêcia kupuj¹. 
W momencie kiedy kupuj¹cy zaakceptuje swoja decyzje staje siê w³aœcicielem praw autorskich.

## Wymagania ogólne
System powinien pozwoliæ artystom na dodawanie i zarz¹dzanie zdjêciami w katalogu poprzez stronê www.  
Katalog ten powinien byæ dostêpny równie¿ dla grafików którzy bêd¹ z niego wybieraæ zdjêcia. 
Sk³adane zamówienie bêdzie równie¿ realizowane przez stronê www. Proces kompletowania zamówienia mo¿e 
trwaæ wiele dni. 
Konto klient mo¿e do³adowaæ swoje konto prepaid.  
P³atnoœci s¹ importowane z banku przez dedykowane API. 
Jest to realizowane 3 razy dziennie. Dla zamówieñ w pe³ni op³aconych system wystawia fakturê i zleca 
wysy³kê zdjêæ w formie papierowej. Graficy s¹ informowani mailowo o zrealizowanej wysy³ce zdjêæ.

## Wymagania szczegó³owe ( strona kupuj¹ca )
Zakres prac bêdzie dotyczy³ obs³ugi zamówieñ strony kupuj¹cej. Kupuj¹cy bêdzie mia³ mo¿liwoœæ wstêpnej 
rezerwacji zdjêæ

1. 
Klienci mog¹ dodawaæ zdjêcia do zamówienia z katalogu. Kilkukrotne dodanie do zamówienia tego samego 
zdjêcia nie ma sensu. System uniemo¿liwia równie¿ kilkakrotny zakup (ró¿ne zamówienia) tego samego 
zdjêcia przez jednego u¿ytkownika.

2. 
Klienci mog¹ poddaæ zamówienie zmianom zanim ostatecznie jest zatwierdz¹. Zamówienie nie jest wi¹¿¹ce 
do momentu zatwierdzenia. Jest w zasadzie wstêpn¹ rezerwacj¹ produktów. 

W trakcie pracy nad rezerwacj¹ ceny zdjêæ mog¹ ulec zmianie. Zasada jest taka, ¿e klient zawsze rezerwuje 
produkty po cenach aktualnie obowi¹zuj¹cych w systemie (a nie takich jakie widzia³), ale musi równie¿ 
widzieæ ceny jakie zostan¹ zastosowane. 

Klienci przed zatwierdzeniem rezerwacji musz¹ zobaczyæ aktualny ofertê do zamówienia jak¹ zaproponuje 
mu system.  Oferta powinien zawieraæ produkty wraz z aktualnymi cenami (a w przysz³oœci 
równie¿ z aktualnie obowi¹zuj¹cymi promocjami).  Mo¿e zajœæ sytuacja gdy dzie³o jest ju¿ 
niedostêpne (np. z powodu usuniêcia z katalogu). Oczywiœcie klient powinien widzieæ 
taki produkt, ale jako nieaktywny.

Oferta nie jest zobowi¹zuj¹ca, klient mo¿e zatwierdziæ lub odrzuciæ tak¹ ofertê. Zatwierdzaj¹c 
ofertê dokonujemy formalnego zakupu. Je¿eli oferta, któr¹ widzia³ klient jest inna ni¿ obowi¹zuj¹ca 
oferta, to nie mo¿e dojœæ do zakupu a klient powinien wówczas zobaczyæ now¹ ofertê. 

3.
Gdy klient zatwierdza zamówienie (ofertê) to wówczas system nie zezwala na zakupy je¿eli klient nie 
posiada dostatecznej iloœci pieniêdzy (konto jest do³adowane w formie prepaid przez zewnêtrzny system).

4. 
System powinien przechowywaæ podpisan¹ cyfrowo historiê zakupów klienta (co, kiedy i za ile) jako ew. 
dowód w postêpowaniu s¹dowym. Dla ka¿dego zakupu powinien byæ generowany fakt zakupu,  który odzwierciedla 
ceny z oferty i nie podlega ju¿ zmianom gdy zmieniaj¹ siê ceny katalogowe i rabaty.

5. 
Podczas zakupu sprawdzamy czy klient posiada dostateczn¹ iloœæ œrodków (credit). Je¿eli nie, to nie mo¿e 
dokonaæ zakupu, chyba, ¿e jest klientem VIP, wówczas udzielamy mu kredytu, chyba, ¿e przekroczy³ 
limit kredytów. Je¿eli klient posiada dostateczn¹ iloœæ œrodków (wg powy¿szych regu³) to pobieramy 
op³atê i odnotowujemy fakt p³atnoœci.


