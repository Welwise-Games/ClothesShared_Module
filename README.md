Общий модуль для модулей одежды.

<b>Установка</b>
1. Установите модуль: https://github.com/Welwise-Games/Shared_Module
2. Установите пакет по пути Package Manager -> Add package from git URL -> https://github.com/Welwise-Games/ClothesShared_Module.git

<b>Код</b><br>
Если класс пишется с маленькой буквы, значит имеется ввиду название инстанса. 

EntryPoint:<br>
Создайте и сохраните инстанс itemsConfigsProviderService и clothesFactory.

При создании игрока:<br>
Вызовите NicknameSharedTools.GetPlayerColorableClothesViewController(itemsConfig, clothesFactory, serializableComponents, equippedItemsData), где itemsConfigs получается вызовом itemsConfigsProviderService.GetItemsConfigAsync(),
serializableComponents - получив компонент у игрока (пример заполнения можно посмотреть в папке Runtime/Client/Example/Example.prefab), а equippedItemsData например из сдк.
