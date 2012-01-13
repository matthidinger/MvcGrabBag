jQuery.validator.unobtrusive.adapters.add("requiredfileupload", function (rule) {

    var message = rule.Message;

    return function (value, context) {
        alert(value);
        return false;
        if (!value || !value.length) {
            // return valid if value not specified - leave that to the 'required' validator 
            return true;
        }

        var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
        return emailPattern.test(value);
    };
});