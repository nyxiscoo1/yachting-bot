namespace YachtingBot;

public class QuestionSource
{
    public IReadOnlyList<IntermediateQuestionInfo> LoadQuestions()
    {
        return LoadQuestionsImpl().SelectMany(x => x.ToQuestions()).ToArray();
    }

    private IEnumerable<ISerializedQuestion> LoadQuestionsImpl()
    {
        // 1
        yield return new MatchingQuestion
        {
            Text = "Сопоставьте международные документы разделам судоходства к которому они относятся",
            Variants = new Dictionary<string, string>
            {
                ["Стандарты обучения и квалификации экипажа"] = "STCW",
                ["Стандарты оборудования судов средствами связи и спасения"] = "GMDSS",
                ["Правила предупреждения столкновения судов"] = "COLREG",
                ["Стандарты оборудования судов спасательными средствами"] = "SOLAS",
            }
        };

        // 2
        yield return new MatchingQuestion
        {
            Text = "Сопоставьте флаги их значению ",
            Variants = new Dictionary<string, string>
            {
                ["/2_1.jpg"] = "Запрос места на таможне",
                ["/2_2.jpg"] = "Флаг бедствия",
                ["/2_3.jpg"] = "Флаг страны регистрации судна",
                ["/2_4.jpg"] = "Флаг страны, в терр водах которого находится судно",
            }
        };

        // 3
        yield return new MatchingQuestion
        {
            Image = "/3.jpg",
            Text = "Сопоставьте флаги месту их размещения на судне",
            Variants = new Dictionary<string, string>
            {
                ["Гостевой флаг"] = "Выше карантинного флага",
                ["Карантинный флаг"] = "Под правой краспицей",
                ["Флаг регистрации судна"] = "На корме",
                ["Клубный флаг"] = "Под левой краспицей",
            }
        };

        // 4
        yield return new MultiAnswerQuestion
        {
            Image = null,
            Text = "Назовите не менее 5 документов, которые должны быть на борту чартерной яхты при плавании в чужих территориальных водах",
            Variants = [
                "Страховка",
                "Радиолицензия",
                "Чартерный договор",
                "Транзит лог",
                "Судовая роль",
                "Сертификат шкипера",
                "Паспорта членов экипажа",

            ],
            RightAnswers = [0, 1, 2, 3, 4, 5, 6],
            AnswerThreshold = 5,
            NoKeyboard = true
        };

        // 5
        yield return new MultiAnswerQuestion
        {
            Image = null,
            Text = "В какие службы нужно обратиться капитану яхты для оформления входа в территориальные воды другого государства?",
            Variants = [
                "Начальник порта",
                "Пограничная служба",
                "Таможенная служба"
            ],
            RightAnswers = [0, 1, 2],
            NoKeyboard = true
        };

        // 6
        yield return new SimpleQuestion
        {
            Image = null,
            Text = "Где можно оформить документы для входа-выхода из территориальных вод государства?",
            Variants = [
                "Entries ports",
                "Таможенно-пограничный порт",
                "Special Ports",
                "Любая крупная марина"
            ],
            RightAnswer = 0
        };

        // 7
        yield return new SimpleQuestion
        {
            Image = null,
            Text = "В каких случаях полиция может проводить обыск и арест на борту судна в море?",
            Variants = [
                "Если судно имеет флаг регистрации отличный от флага территориальных вод",
                "Если судно вызывает подозрение",
                "Если судно имеет флаг регистрации совпадающий с флагом территориальных вод"
            ],
            RightAnswer = 2
        };

        // 8
        yield return new SimpleQuestion
        {
            Image = null,
            Text = "Обязано ли судно оказать помощь другому судну, подавшему сигнал бедствия, если оно находится в доступной близости? ",
            Variants = [
                "может участвовать",
                "обязано",
                "не обязано"
            ],
            RightAnswer = 1
        };

        // 9
        yield return new SimpleQuestion
        {
            Image = null,
            Text = "В каком случае капитан ближайшего к терпящему бедствие судну имеет право отказаться от спасения имущества (включая спасение самого судна)?",
            Variants = [
                "Если не удалось договоориться о размере вознаграждения",
                "Если в силу каких-то обстоятельств участие в спасении имущества представляется для спасателей слишком сложным",
                "Если участие в спасении имущества представляет угрозу для сохранности судна и жизни его экипажа",
                "Не имеет права отказаться"
            ],
            RightAnswer = 2
        };

        // 10
        yield return new SimpleQuestion
        {
            Image = null,
            Text = "Какие документы обязаны иметь члены экипажа яхты, выходящей в чужие территориальные воды с целью выхода на берег? ",
            Variants = [
                "Достаточно загранпаспорта",
                "Нужен паспорт моряка",
                "Достаточно загранпаспорта и крю листа",
                "Должны быть действующие туристические визы в действующем загранпаспорте"
            ],
            RightAnswer = 3
        };

        // 11
        yield return new MultiAnswerQuestion
        {
            Image = null,
            Text = "Кто будет оплачивать спасательную операцию?",
            Variants = [
                "Собственник судна",
                "Капитан судна",
                "Государство",
                "За счет страхового депозита",
                "Все члены команды спасенного судна",
            ],
            RightAnswers = [0, 2, 3]
        };

        // 12
        yield return new SimpleQuestion
        {
            Image = null,
            Text = "Ваши действия если при маневрировании вы задели чужую яхту?",
            Variants = [
                "Продолжать движение в море, страховые сами разберутся",
                "Связаться с начальником порта и сообщить об инциденте",
                "Вернуться и разыскать хозяина яхты, чтоб урегулировать убыток"
            ],
            RightAnswer = 1
        };

        // 13 
        yield return new SimpleQuestion
        {
            Image = null,
            Text = "На яхте идущей из Турции в Черногорию сломался двигатель в акватории Греции, может ли яхта остановиться в порту Греции на ремонт при отсутствии у экипажа Шенгенских виз?",
            Variants = [
                "Нет, яхту не пустят на стоянку",
                "Да, но только на 48 часов без выхода на берег ",
                "Да, разрешена стоянка и выход на берег в экстренной ситуации"
            ],
            RightAnswer = 1
        };

        // 14
        yield return new SimpleQuestion
        {
            Image = null,
            Text = "Можно ли пользоваться гаьюном в марине? ",
            Variants = [
                "Нет, ни в коем случае, только береговые туалеты",
                "Да, как обычно",
                "Можно, при условии закрытых фекальных танках"
            ],
            RightAnswer = 2
        };


        // 15
        yield return new MatchingQuestion
        {
            Text = "Сопоставьте, при каких условиях какой мусор можно сбрасывать за борт?",
            Variants = new Dictionary<string, string>
            {
                ["Черные воды"] = "Не ближе 4 миль от берега",
                ["Стекло "] = "Иногда, в дальних походах",
                ["Пластик"] = "Никогда",
                ["Кожура от бананов, огрызки"] = "Можно, но не в рекреационных зонах",
            }
        };
        //  16
        yield return new SimpleQuestion
        {
            Image = "/6.jpg",
            Text = "Выберите название части яхты указанное цифрой 1",
            Variants = [
               "Гик",
                "Аппарель",
                "Форштаг",
                "Ванта"

           ],
            RightAnswer = 2
        };
        yield return new SimpleQuestion
        {
            Image = "/6.jpg",
            Text = "Выберите название части яхты указанное цифрой 2",
            Variants = [
                "Гик",
                "Рубка",
                "Леер",
                "Ванта"

        ],
            RightAnswer = 0
        };
        yield return new SimpleQuestion
        {
            Image = "/6.jpg",
            Text = "Выберите название части яхты указанное цифрой 3",
            Variants = [
                "Леер",
                "Рубка",
                "Аппарель",
                "Ванта"

      ],
            RightAnswer = 2
        };
        yield return new SimpleQuestion
        {
            Image = "/6.jpg",
            Text = "Выберите название части яхты указанное цифрой 4",
            Variants = [
                "Кокпит",
                "Реллинг",
                "Аппарель",
                "Леер"

 ],
            RightAnswer = 3
        };
        yield return new SimpleQuestion
        {
            Image = "/6.jpg",
            Text = "Выберите название части яхты указанное цифрой 5",
            Variants = [
                "Кокпит",
                "Реллинг",
                "Аппарель",
                "Леер"

],
            RightAnswer = 1
        };
        yield return new SimpleQuestion
        {
            Image = "/6.jpg",
            Text = "Выберите название части яхты указанное цифрой 6",
            Variants = [
                 "Ванта",
                "Реллинг",
                "Аппарель",
                "Леер"

],
            RightAnswer = 0
        };
        yield return new SimpleQuestion
        {
            Image = "/6.jpg",
            Text = "Выберите название части яхты указанное цифрой 7",
            Variants = [
                "Ванта",
                "Реллинг",
                "Бак",
                "Леер"

],
            RightAnswer = 2
        };
        yield return new SimpleQuestion
        {
            Image = "/6.jpg",
            Text = "Выберите название части яхты указанное цифрой 8",
            Variants = [
                "Аппарель",
                "Реллинг",
                "Кокпит",
                "Рубка"

],
            RightAnswer = 3
        };
        yield return new SimpleQuestion
        {
            Image = "/6.jpg",
            Text = "Выберите название части яхты указанное цифрой 9",
            Variants = [
                "Бак",
                "Реллинг",
                "Кокпит",
                "Рубка"

],
            RightAnswer = 2
        };
        //  17
        yield return new SimpleQuestion
        {
            Image = "/5.jpg",
            Text = "Выберите название части яхты указанное цифрой 1",
            Variants = [
               "Грот",
                "Краспицы",
                "Ахтерштаг",
                "Перо"

           ],
            RightAnswer = 0
        };
        yield return new SimpleQuestion
        {
            Image = "/5.jpg",
            Text = "Выберите название части яхты указанное цифрой 2",
            Variants = [
              "Грот",
                "Краспицы",
                "Ахтерштаг",
                "Перо"

          ],
            RightAnswer = 2
        };
        yield return new SimpleQuestion
        {
            Image = "/5.jpg",
            Text = "Выберите название части яхты указанное цифрой 3",
            Variants = [
              "Топ",
                "Ванты",
                "Ахтерштаг",
                "Гик"

          ],
            RightAnswer = 3
        };

        yield return new SimpleQuestion
        {
            Image = "/5.jpg",
            Text = "Выберите название части яхты указанное цифрой 4",
            Variants = [
                 "Топ",
                "Ванты",
                "Перо",
                "Гик"

  ],
            RightAnswer = 2
        };

        yield return new SimpleQuestion
        {
            Image = "/5.jpg",
            Text = "Выберите название части яхты указанное цифрой 5",
            Variants = [
                 "Киль",
                "Бульб",
                "Ахтерштаг",
                "Перо"

  ],
            RightAnswer = 0
        };
        yield return new SimpleQuestion
        {
            Image = "/5.jpg",
            Text = "Выберите название части яхты указанное цифрой 6",
            Variants = [
              "Киль",
                "Бульб",
                "Ахтерштаг",
                "Форштаг"

],
            RightAnswer = 1
        };
        yield return new SimpleQuestion
        {
            Image = "/5.jpg",
            Text = "Выберите название части яхты указанное цифрой 7",
            Variants = [
           "Краспицы",
                "Топ",
                "Ахтерштаг",
                "Ванты"

],
            RightAnswer = 3
        };

        yield return new SimpleQuestion
        {
            Image = "/5.jpg",
            Text = "Выберите название части яхты указанное цифрой 8",
            Variants = [
              "Киль",
                "Бульб",
                "Краспицы",
                "Перо"

],
            RightAnswer = 2
        };

        yield return new SimpleQuestion
        {
            Image = "/5.jpg",
            Text = "Выберите название части яхты указанное цифрой 9",
            Variants = [
           "Киль",
                "Бульб",
                "Ахтерштаг",
                "Форштаг"

],
            RightAnswer = 3
        };

        yield return new SimpleQuestion
        {
            Image = "/5.jpg",
            Text = "Выберите название части яхты указанное цифрой 10",
            Variants = [
           "Топ",
                "Бульб",
                "Киль",
                "Форштаг"

],
            RightAnswer = 0
        };
        //  18
        yield return new SimpleQuestion
        {
            Image = "/7.jpg",
            Text = "Выберите название части паруса указанное цифрой 1",
            Variants = [
               "Фаловый угол",
                "Галсовый угол",
                "Шкотовый угол",


           ],
            RightAnswer = 0
        };
        yield return new SimpleQuestion
        {
            Image = "/7.jpg",
            Text = "Выберите название части паруса указанное цифрой 2",
            Variants = [
           "Фаловый угол",
                "Уф-защита",
                "Передняя шкаторина",

           ],
            RightAnswer = 1
        };
        yield return new SimpleQuestion
        {
            Image = "/7.jpg",
            Text = "Выберите название части паруса указанное цифрой 3",
            Variants = [
        "Нижняя шкаторина",
                "Грот",
                "Передняя шкаторина",

           ],
            RightAnswer = 2
        };
        yield return new SimpleQuestion
        {
            Image = "/7.jpg",
            Text = "Выберите название части паруса указанное цифрой 4",
            Variants = [
              "Фаловый угол",
                "Стаксель",
                "Латы",

           ],
            RightAnswer = 1
        };
        yield return new SimpleQuestion
        {
            Image = "/7.jpg",
            Text = "Выберите название части паруса указанное цифрой 5",
            Variants = [
        "Нижняя шкаторина",
                "Задняя шкаторина",
                "Передняя шкаторина",

           ],
            RightAnswer = 1
        };
        yield return new SimpleQuestion
        {
            Image = "/7.jpg",
            Text = "Выберите название части паруса указанное цифрой 6",
            Variants = [
              "Фаловый угол",
                "Галсовый угол",
                "Шкотовый угол",

           ],
            RightAnswer = 1
        };
        yield return new SimpleQuestion
        {
            Image = "/7.jpg",
            Text = "Выберите название части паруса указанное цифрой 7",
            Variants = [
   "Нижняя шкаторина",
                "Задняя шкаторина",
                "Передняя шкаторина",

           ],
            RightAnswer = 0
        };
        yield return new SimpleQuestion
        {
            Image = "/7.jpg",
            Text = "Выберите название части паруса указанное цифрой 8",
            Variants = [
                "Фаловый угол",
                "Галсовый угол",
                "Шкотовый угол",
           ],
            RightAnswer = 2
        };
        yield return new SimpleQuestion
        {
            Image = "/7.jpg",
            Text = "Выберите название части паруса указанное цифрой 9",
            Variants = [
          "Грот",
                "Стаксель",
                "Латы",

           ],
            RightAnswer = 0
        };
        yield return new SimpleQuestion
        {
            Image = "/7.jpg",
            Text = "Выберите название части паруса указанное цифрой 10",
            Variants = [
    "Нижняя шкаторина",
                "Задняя шкаторина",
                "Передняя шкаторина",

           ],
            RightAnswer = 0
        };
        yield return new SimpleQuestion
        {
            Image = "/7.jpg",
            Text = "Выберите название части паруса указанное цифрой 11",
            Variants = [
   "Шкотовыйугол",
                "Галсовый угол",
                "Фаловый угол",

           ],
            RightAnswer = 0
        };
        yield return new SimpleQuestion
        {
            Image = "/7.jpg",
            Text = "Выберите название части паруса указанное цифрой 12",
            Variants = [
"Нижняя шкаторина",
                "Задняя шкаторина",
                "Передняя шкаторина",

           ],
            RightAnswer = 2
        };
        yield return new SimpleQuestion
        {
            Image = "/7.jpg",
            Text = "Выберите название части паруса указанное цифрой 13",
            Variants = [
"Нижняя шкаторина",
                "Задняя шкаторина",
                "Передняя шкаторина",

           ],
            RightAnswer = 1
        };
        yield return new SimpleQuestion
        {
            Image = "/7.jpg",
            Text = "Выберите название части паруса указанное цифрой 14",
            Variants = [
      "Грот",
                "Стаксель",
                "Латы",

           ],
            RightAnswer = 2
        };
        yield return new SimpleQuestion
        {
            Image = "/7.jpg",
            Text = "Выберите название части паруса указанное цифрой 15",
            Variants = [
"Шкотовыйугол",
                "Галсовый угол",
                "Фаловый угол",

           ],
            RightAnswer = 2
        };
        //  19
        yield return new SimpleQuestion
        {
            Image = "/8.jpg",
            Text = "Выберите название дельной вещи А",
            Variants = [
               "Полуклюз",
                "Каретка",
                "Мочка",
                "Пианино",

           ],
            RightAnswer = 2
        };
        yield return new SimpleQuestion
        {
            Image = "/8.jpg",
            Text = "Выберите название дельной вещи В",
            Variants = [
         "Полуклюз",
                "Каретка",
                "Мочка",
                "Утка",

           ],
            RightAnswer = 3
        };
        yield return new SimpleQuestion
        {
            Image = "/8.jpg",
            Text = "Выберите название дельной вещи С",
            Variants = [
                   "Полуклюз",
                "Каретка",
                "Пианино",
                "Утка",

           ],
            RightAnswer = 0
        };
        yield return new SimpleQuestion
        {
            Image = "/8.jpg",
            Text = "Выберите название дельной вещи D",
            Variants = [
          "Полуклюз",
                "Каретка",
                "Мочка",
                "Пианино",

           ],
            RightAnswer = 3
        };
        yield return new SimpleQuestion
        {
            Image = "/8.jpg",
            Text = "Выберите название дельной вещи Е",
            Variants = [
         "Полуклюз",
                "Каретка",
                "Мочка",
                "Пианино",

           ],
            RightAnswer = 1
        };
        yield return new SimpleQuestion
        {
            Image = "/8.jpg",
            Text = "Выберите название дельной вещи F",
            Variants = [
           "Полуклюз",
                "Блоки",
                "Утка",
                "Пианино",

           ],
            RightAnswer = 1
        };

        // 3-1
        //yield return new MatchingQuestion
        //{
        //    Text = "Расставьте возможные ЧП по степени вероятности наступления на борту яхты. Первым - наиболее вероятное событие.",
        //    Variants = new Dictionary<string, string>
        //    {
        //        ["Пожар"] = "STCW",
        //        ["МОВ (человек за бортом)"] = "GMDSS",
        //        ["Травма членов экипажа"] = "COLREG",
        //        ["Затопление"] = "SOLAS",
        //        ["Критические поломки"] = "SOLAS",
        //    }
        //};

        // 3-2
        yield return new MatchingQuestion
        {
            Text = "Какие средства безопасности предназначены для борьбы с каким типом ЧП",
            Variants = new Dictionary<string, string>
            {
                ["Чопики"] = "Борьба с затоплением",
                ["УКВ станция "] = "Средство для подачи сигнала",
                ["Кошма"] = "Противопожарное оборудование",
                ["Дэн буй"] = "Средство обозначения человека на воде",
                ["Обвязка"] = "Средство для предотвращения падения экипажа за борт",
            }
        };

        // 3-3
        yield return new MatchingQuestion
        {
            Text = "Какие средства безопасности предназначены для борьбы с каким типом ЧП",
            Variants = new Dictionary<string, string>
            {
                ["3-3_1.jpg"] = "Подходит для водных видов спорта",
                ["3-3_2.jpg"] = "Компактный, может быть совмещен с обвязкой, ежегодно нужно проводить обслуживание",
                ["3-3_3.jpg"] = "Спасательный жилет по требованиям SOLAS",
            }
        };

        //  3-5
        yield return new SimpleQuestion
        {
            Image = null,
            Text = "Для чего нужен рефлектор на яхте?",
            Variants = [
               "Средство борьбы с затоплением",
                "Средство для идентификации яхты на радарах других судов",
                "Средство определения месторасположения судна",
                "Средство для идентификации препятствий на пути судна",

           ],
            RightAnswer = 1
        };

        // 3-6
        yield return new MatchingQuestion
        {
            Text = "Сопоставьте значения и колонки: каким огнетушителем какую категорию материалов следует тушить?",
            Variants = new Dictionary<string, string>
            {
                ["Пудра\\Powder"] = "Категория А, Категория В, Категория С (газ), Категория Е (электрика)",
                ["Карбон\\CO2"] = "Категория В, Категория Е",
                ["Пенный огнетушитель\\Foam"] = "Категория А, Категория В (горючие жидкости)",
                ["Вода"] = "Категория А (твердые материалы)",
            }
        };

        //  3-7
        yield return new SimpleQuestion
        {
            Image = null,
            Text = "Ваше судно терпит бедствие ясной ночью недалеко от судоходного маршрута. Какое средство пиротехники вы используете для подачи сигнала SOS?",
            Variants = [
              "Фальшвеер",
             "Дымовая шашка",
             "Красная парашютная ракета",
            "Белая ракета",
           ],
            RightAnswer = 2
        };

        //  3-8
        yield return new SimpleQuestion
        {
            Image = null,
            Text = "Сколько ракет вы будете выпускать для привлечения внимания и с каким интервалом?",
            Variants = [
             "Две ракеты с интервалом в 2 минуты",
          "Три ракеты с интервалом в минуту",
           "Одну ракету",
           "Две ракеты с интервалом в минуту",

           ],
            RightAnswer = 1
        };

        // 3-9
        yield return new MatchingQuestion
        {
            Text = "Какие средства безопасности предназначены для борьбы с каким типом ЧП",
            Variants = new Dictionary<string, string>
            {
                ["В поисках МОВ в ночное время"] = "Белый фальшвеер ",
                ["Указание своего местоположения в видимости спасателей "] = "Красный фальшвеер",
                ["Сигнал distress вне берегов и видимости других судов "] = "Красная парашютная ракета",
                ["Предупреждение другого судна о риске столкновения "] = "Белая ракета",
                ["Обозначение позиции для самолета в дневное время "] = "Оранжевая дымовая шашка",
            }
        };

        // 3-10
        //yield return new MultiAnswerQuestion
        //{
        //    Image = null,
        //    Text = "Напишите 4 важных пункта действий, когда человек за бортом",
        //    Variants = [
        //        "",
        //        "",
        //        "",
        //        "",

        //    ],
        //    RightAnswers = [0, 1, 2, 3, 4],
        //    NoKeyboard = true
        //};

        //  23
        yield return new MultiAnswerQuestion
        {
            Image = null,
            Text = "В каких случаях и кто может подать сигнал MayDay на яхте?",
            Variants = [
               "Только капитан",
               "Любой член экипажа",
               "Экипаж, если капитан не может исполнять свои функции",
               "Капитан или член экипажа по просьбе капитана",
           ],
            RightAnswers = [2, 3]
        };
        //  24
        yield return new MultiAnswerQuestion
        {
            Image = null,
            Text = "Когда нажимают кнопку передачи сигнала сос-Distress на укв радиостанции?",
            Variants = [
               "В случае возникновения чп",
               "Когда экипаж чувствует приближение беды",
               "По прямому указанию капитана",
               "При неминуемой угрозе жизни",
               "Когда капитан не адекватен",

           ],
            RightAnswers = [2, 3]
        };
        //  24
        yield return new MultiAnswerQuestion
        {
            Image = null,
            Text = "Какими способами можно подать сигнал MAYDAY на яхте?",
            Variants = [
            "Флагами Novemer Golf",
            "С помощью укв станции",
            "С помощью пиротехники",
            "С помощью электронной навигации",
            "С помощью последовательного взмаха рук",


           ],
            RightAnswers = [1, 2, 4]
        };
    }
}
