$("#dropdown").change(function () {

    var selectValue = parseInt($(this).val());

    var context = $("#context");

    var Objul = $("<ul></ul>").addClass("list-group")

    for (var i = 0; i < selectValue; i++) {
        var Objli = $('<li></li>').addClass("list-group-item").css("width", "25%");
        var ObjDiv = $('<div></div>')

        $('<h4/>').attr({ id: 'Title' }).addClass("text-center").text(i + 1 + " Bill").appendTo(ObjDiv);
        $('<label/>').attr({ id: 'lableIBank'}).css("margin-right", "5%").text("IBank").appendTo(ObjDiv);
        $('<input/>').attr({ type: 'text', id: 'IBank' + i }).appendTo(ObjDiv);
        $('<label/>').attr({ id: 'lableMoney' }).css("margin-right", "5%").text("Money").appendTo(ObjDiv);
        $('<input/>').attr({ type: 'text', id: 'Money' + i }).appendTo(ObjDiv);

        Objli.append(ObjDiv)
        Objul.append(Objli)
    }

    context.html("");
    context.append(Objul)

    var ObjDivToBill = $('<div></div>')
    $('<label/>').attr({ id: 'lableEmail'}).css("margin-right", "2%").text("Email").appendTo(ObjDivToBill);
    $('<input/>').attr({ type: 'text', id: 'Email' }).css("margin-right", "3%").appendTo(ObjDivToBill);
    $('<label/>').attr({ id: 'lableIBankOnBillToPutMoney' }).css("margin-right", "2%").text("IBank on your bill").appendTo(ObjDivToBill);
    $('<input/>').attr({ type: 'text', id: 'IBankOnBillToPutMoney' }).appendTo(ObjDivToBill);

    context.append(ObjDivToBill)
    context.append($('<button/>').attr({ onClick: 'SentJson()' }).addClass("btn btn-success").css("margin-left", "25%").text("Put Money"))

});

function SentJson() {

    var selectValue = parseInt($("#dropdown").val());

    var model = {
        PutMoneyFromManyBillsModel:
            {
                Email: $("#Email").val(),
                Bills: [],
                IBankOnBillSetMoney: $("#IBankOnBillToPutMoney").val()
            }

    }

    for (var i = 0; i < selectValue; i++) {
        var idIBank = '#IBank' + i;
        var idIMoney = '#Money' + i;

        var money = $(idIMoney).val();
        var ibank = $(idIBank).val();
        
        model.PutMoneyFromManyBillsModel.Bills.push(
        {
            Money: money,
            IBankOnBillFromGetMoney: ibank
        }
        )
    }

    var json = JSON.stringify(model.PutMoneyFromManyBillsModel).toString()

    $.ajax({
        url: "/Bill/PutMoneyFromManyBill",
        type: "POST",
        data: JSON.stringify({ model: json }),
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            debugger;

            if (data.status == "Success") {
                alert("Succes");
                $.ajax({
                    url: "/Home/Index",
                    type: "Get",
                    success: function (data) {
                    }
                });
            }
           
            else if (data.status == "no exist bill") {
                alert("no exist bill");
            }
            else if (data.status == "no exist really bill") {
                alert("no exist really bill");
            }
            else if (data.status == "no balance in really bill") {
                alert("no balance in really bill");
            }
            else {
                alert("Error occurs on the Database level!");
            }
        },
        error: function (data ) {
            alert("An error has occured!!!");
        }
    });
}