

var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44379/offerHub",).build();

connection.start().then(res => console.log('Hub bağlantısı başarılı')).catch(err => console.log('Huba bağlanırken hata oluştu!'));

connection.on("SEND_OFFER", function (message) {
    
    populateTable(message.toString());
});


$(document).ready(function () {
    $("#IdPlate").change(function () {
        if ($(this).val().length > 5 && $("#IdTckn").val().length === 11) {
            getUserInfo();
        }
    });

    $("#IdPlate").change(function () {
        if ($(this).val().length > 5 && $("#IdTckn").val().length === 11) {
            getUserInfo();
        }
    });
});

function getUserInfo() {
    $.ajax({
        type: "GET",
        url: 'https://localhost:44379/api/Insurance/User?Plate=' + $("#IdPlate").val() + '&IdentityNumber=' + $("#IdTckn").val(),
        success: function (result) {
            if (result) {
                console.log(result)
                $("#IdSerialCode").val(result.licenseSerialCode);
                $("#IdSerialNo").val(result.licenseSerialNo);
            } else {
                alert('Hataa');
            }
        }
    });
}


function getOffer() {

    var postModel = {
        Plate: $("#IdTckn").val(),
        IdentityNumber: $("#IdPlate").val(),
        LicenseSerialCode: $("#IdSerialCode").val(),
        LicenseSerialNo: $("#IdSerialNo").val()
    }

    $.ajax({
        type: 'POST',
        dataType: "json",
        url: 'https://localhost:44379/api/Insurance/Offer',
        data: JSON.stringify(postModel),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        success: function (result) {
            if (result) {
                if (!result.success) {
                    alert("Teklif Hazırlanıyor.");
                } else {
                    alert("Teklif hazırlanırken hata oluştu");
                }
            }
            else {
                alert("Hata!!!!");
            }
        },
        
    });


}

function checkOfferStatus(result) {
    $.ajax({
        type: "Get",
        url: 'https://localhost:44379/api/Insurance/Offer/Process/' + result.processId,
        success: function (res) {
            if (res) {
                console.log('Api isteği başarılı');
            } else {
                alert('Teklifler yüklenirken hata oluştu!');
            }
        }
    });
}

function getUserOffer2() {
    
    $.ajax({
        type: "Get",
        url: 'https://localhost:44379/api/Insurance/Offers/' + $("#IdTckn").val(),
        success: function (result) {
            if (result) {
                console.log(result);
                //getUserOffer2();
            } else {
                alert('hata');
            }
        }
    });
}


function getUserOfferHistory() {

    $(document).ready(function () {
        $('#offerTable').DataTable({
            processing: true,
            serverSide: false,
            ordering: false,
            paging: false,
            searching: false,
            columns: [
                { data: "CompanyName" },
                { data: "Description" },
                { data: "Price" },
                { data: "Plate" },
                { data: "OfferDate" }
            ],
            ajax: {
                'type': "Get",
                'url': 'https://localhost:44379/api/Insurance/Offers/' + $("#IdTckn").val(),
                "dataSrc": ''
            },
            "columnDefs": [
                {
                    "targets": 1,
                    "visible": false
                },
               
            ],
            "bDestroy": true
        });
    });
}


function populateTable(dataSource) {
    
    console.log(dataSource);
    $('#offerQueryTable').DataTable({
        data: JSON.parse(dataSource),
        processing: true,
        serverSide: false,
        ordering: false,
        paging: false,
        searching: false,
        columns: [
            { data: "CompanyName" },
            { data: "Description" },
            { data: "Price" },
            { data: "Plate" },
            { data: "OfferDate" }
        ],
        "columnDefs": [
            {
                "targets": 1,
                "visible": false
            },

        ],
        "bDestroy": true
    });
}
