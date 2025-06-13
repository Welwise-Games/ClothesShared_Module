Общий модуль для модулей одежды.

<b>Установка</b>
1. Установите модуль: https://github.com/Welwise-Games/Shared_Module
2. Установите пакет по пути Window -> Package Manager -> Add package from git URL -> https://github.com/Welwise-Games/ClothesShared_Module.git
3. После установки перейдите Window -> Asset Management -> Addressables -> Groups. Если настройки не созданы, создайте (большая кнопка в середине окна). 
Переместите файлы Clothes Shared Client Module Group и Clothes Shared Shared Module Group (вы можете найти их в поиске в окне Project в unity) в окно Groups через Drag and drop.

<b>Пример</b><br>
По пути Runtime/Client/Example/ExampleScene (внутри сцены) есть персонаж, на котором есть компонент ClothesLoader - он загружает одежду из сдк при запуске сцены. Для открытия сцены скопируйте её в папку Assets и откройте эту новую сцену.
Также на нём есть компонент PlayerColorableClothesViewSerializableComponents, где в полях компоненты MainSkinnedMeshRenderer - это любая часть персонажа с компонентом Skinned Mesh Renderer,
а DefaultClothesInstances - дефолтные части одежды, которые будут выключены, если на персонаже есть другая одежда той же категории (у персонажа в примере также всё указано). 

<b>Код</b><br>
Если класс пишется с маленькой буквы, значит имеется ввиду название инстанса. 

EntryPoint:<br>
Создайте и сохраните инстанс itemsConfigsProviderService и clothesFactory.

При создании игрока:<br>
Вызовите ClothesSharedTools.GetPlayerColorableClothesViewController(itemsConfig, clothesFactory, serializableComponents, equippedItemsData), где itemsConfigs получается вызовом itemsConfigsProviderService.GetItemsConfigAsync(),
serializableComponents - получив компонент у игрока (пример заполнения можно посмотреть в папке Runtime/Client/Example/Example.prefab), а equippedItemsData например из сдк.
