var ObjectState = {
    Unchanged : 0,
    Added : 1,
    Modified : 2,
    Deleted : 3
}

var salesOrderItemMapping = {
    'SalesOrderItems' : {
        key: function(salesOrderItem){
            return ko.utils.unwrapObservable(salesOrderItem.SalesOrderItemId);
        },
        create: function (options) {
            return new SalesOrderItemViewModel(options.data);
        }
    }
};



SalesOrderItemViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);
};

SalesOrderViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self),

    self.save = function () {
        $.ajax({
            url: '/Sales/Save/',
            type: 'POST',
            data: ko.toJSON(self),
            contentType: 'application/json',
            success: function (data) {
                if (data.salesOrderViewModel != null)
                    ko.mapping.fromJS(data.salesOrderViewModel, {}, self);

                
                if(data.newLocation!=null)
                    window.location.reload(data.newLocation);
            },
            error: function (data) {
                alert("Error");
            }
        });
    },

    self.flagSalesOrderAsEdited = function () {
        if (self.ObjectState() != ObjectState.Added){
            self.ObjectState(ObjectState.Modified);
        }
        
        return true;
    }
}