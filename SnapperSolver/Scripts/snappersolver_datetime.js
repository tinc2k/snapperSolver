function GetDecentDate() {
    var date = new Date();
    var date_string = "";
    /*year*/
    date_string += date.getFullYear();
    /*month*/
    var month = date.getMonth() + 1;
    month < 10 ? date_string += '0' + month : date_string += month;
    /*day*/
    var day = date.getDate();
    day < 10 ? date_string += '0' + day + ' ' : date_string += day + ' ';
    /*hour*/
    var hours = date.getHours();
    hours < 10 ? date_string += '0' + hours + ':' : date_string += hours + ':';
    /*minute*/
    var minutes = date.getMinutes();
    minutes < 10 ? date_string += '0' + minutes + ':' : date_string += minutes + ':';
    /*second*/
    var seconds = date.getSeconds();
    seconds < 10 ? date_string += '0' + seconds : date_string += seconds;

    return date_string;
}
