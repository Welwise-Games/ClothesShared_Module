Общий модуль для модулей одежды.

Гайд по внедрению в проект:

EntryPoint
Создайте и сохраните инстанс itemsConfigsProviderService и clothesFactory.

При создании игрока
Вызовите NicknameSharedTools.GetPlayerColorableClothesViewController(itemsConfig, clothesFactory, serializableComponents, equippedItemsData), где itemsConfigs получается вызовом itemsConfigsProviderService.GetItemsConfigAsync(),
serializableComponents - получив компонент у игрока (пример заполнения можно посмотреть в папке Runtime/Client/Example/Example.prefab у объекта с именем V3 Character), а equippedItemsData например из сдк.
