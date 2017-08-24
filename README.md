# ThreadsAutoReset

## W tej aplikacji mamy przykład synchroniazacji wątków.

### Program ma za zadnie wyświetlić za pomocą dwóch wątków liczby z podanego zakresu. Jeden z nich jest odpowiedzialny za 
### wyświetlenie liczb nieparzystych, drugi parzystych.

Program otrzymuje następujące dane:

* Zakres dolny - liczba od której ma się zacząć ukazywanie ciągu
* Zakres górny - liczba na której ma się zakończyć pokazywanie ciągu
* Odstęp czasowy - odstęp czasu pomiędzy każdym wyświetleniem liczby w wątku
* Sygnalizacja - jeżeli jest ustawiona wątki są synchronizowane i liczby są wyświetlane w odpowiedniej kolejności, jeżeli nie - kto pierwszy
ten lepszy.

Przydatny kod i klasy które warto ogarnąć

```c#
  private static AutoResetEvent event_1 = new AutoResetEvent(false);
  private static AutoResetEvent event_2 = new AutoResetEvent(false);
```

Odpwiednia manipulacja klasą AutoResetEvent, przybliży zamierzony rezultat synchroznizacji.

 
