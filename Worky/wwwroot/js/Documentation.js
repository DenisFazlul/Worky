function test(){
    alert(getURLVar('pid'));
}

function getURLVar(key) {
    var vars = location.search.substr(1).split('&').reduce(function (res, a) {
        var t = a.split('=');
        res[decodeURIComponent(t[0])] = t.length == 1 ? null : decodeURIComponent(t[1]);
        return res;
    }, {});
    return vars[key] ? vars[key] : '';
}

function OnLoad() {
    var xhr = new XMLHttpRequest();
    var projectId = getURLVar('pid');
    var link = '/Documentation/GetPages?pid=' + projectId;
    alert(link);
    // 2. Конфигурируем его: GET-запрос на URL 'phones.json'
    xhr.open('GET', link, false);

    // 3. Отсылаем запрос
    xhr.send();

    // 4. Если код ответа сервера не 200, то это ошибка
    if (xhr.status != 200) {
        // обработать ошибку
        alert(xhr.status + ': ' + xhr.statusText); // пример вывода: 404: Not Found
    } else {
        // вывести результат
        alert(xhr.responseText); // responseText -- текст ответа.
    }
}