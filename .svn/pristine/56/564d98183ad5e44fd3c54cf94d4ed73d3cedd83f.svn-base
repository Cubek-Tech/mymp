
window.onload = init;
 

function init() {
   
   document.getElementById("reset").onclick = clearDisplay;
   // Set initial focus
   document.getElementById("account_id").focus();
}

function validateForm() {
   return (isNotEmpty("account_id", "Account Id is Required!")
        && isNotEmpty("return_url", "Return URL is Required!")
&& isNotEmpty("reference_no", "Reference number is Required!")
&& isNotEmpty("amount", "Amount is Required!")
&& isNotEmpty("description", "Description is Required!")
&& isNotEmpty("name", "Billing Name is Required!")
&& isNotEmpty("address", "Billing Address is Required!")
&& isNotEmpty("city", "Billing City is Required!")
&& isNotEmpty("state", "Billing State is Required!")
&& isNotEmpty("postal_code", "Billing Postal code is Required!")
&& isNotEmpty("email", "Billing Email is Required!")
&& isNotEmpty("phone", "Billing Phone is Required!")
&& isNotEmpty("ship_name", "Shipping Name is Required!")
&& isNotEmpty("ship_address", "Shipping Address is Required!")
&& isNotEmpty("ship_city", "Shipping City is Required!")
&& isNotEmpty("ship_state", "Shipping State is Required!")
&& isNotEmpty("ship_phone", "Shiping Phone is Required!")
&& isNotEmpty("ship_postal_code", "Shipping Postal code is Required!")
&& isANumeric("amount", "Please Enter valid value for Amount !")
&& isLengthMinMax("postal_code", "Postalcode value limit should be 1 - 10!", 1, 10)
&& isValidEmail("email", "Invalid Email Format!")
&& isNumeric("phone", "Phone Number should be numeric!")
&& isLengthMinMax("phone", "Phone value limit is 5 - 20 !", 5, 20)
&& isLengthMinMax("city", "City max value length is 32 !", 1, 32)
&& isLengthMinMax("state", "State max value length is 32 !", 1, 32)

&& isLengthMinMax("ship_city", "Shipping City max value length is 32 !", 1, 32)
&& isLengthMinMax("ship_state", "Shipping State max value length is 32 !", 1, 32)
&& isLengthMinMax("ship_postal_code", "Shipping Postalcode value limit is 1 - 10 !", 1, 10)
&& isLengthMinMax("ship_phone", "Shipping Phone value limit is 5 - 20 !", 5, 20)
&& isNumeric("ship_phone", "Shipping Phone Number should be numeric!")


&& isCardEmpty("name_on_card", "Name on Card is Required!")
&& isCardEmpty("card_number", "Card Number is Required!")
&& isCardEmpty("card_expiry", "Card Expiry is Required!")
&& isCardEmpty("card_cvv", "Card CVV is Required!")
&& isNumeric("card_number", "Card Number should be numeric!")
&& isNumeric("card_expiry", "Card Expiry should be numeric!")
&& isNumeric("card_cvv", "Card CVV should be numeric!")
&& isLengthMinMax("card_number", "Card Number value limit is 13 - 19 !", 13, 19)
&& isLengthMinMax("card_cvv", "Card CVV value limit is 3 - 4 !", 3, 4)
);
}

 
// Return true if the input value is not empty
function isNotEmpty(inputId, errorMsg) {
   var inputElement = document.getElementById(inputId);
   var errorElement = document.getElementById(inputId + "Error");
   var inputValue = inputElement.value.trim();
   var isValid = (inputValue.length !== 0);  // boolean
   showMessage(isValid, inputElement, errorMsg, errorElement);
   return isValid;
   }
   
   function isCardEmpty(inputId, errorMsg) {
       if (document.getElementById('channel').value === '2')
       {
   var inputElement = document.getElementById(inputId);
   var errorElement = document.getElementById(inputId + "Error");
   var inputValue = inputElement.value.trim();
   var isValid = (inputValue.length !== 0);  // boolean
   showMessage(isValid, inputElement, errorMsg, errorElement);
   return isValid;
  }
   }
function showMessage(isValid, inputElement, errorMsg, errorElement) {
   if (!isValid) {
      // Put up error message on errorElement or via alert()
      if (errorElement !== null) {
         errorElement.innerHTML = errorMsg;
      } else {
         alert(errorMsg);
      }
      // Change "class" of inputElement, so that CSS displays differently
      if (inputElement !== null) {
         inputElement.className = "error";
         inputElement.focus();
      }
   } else {
      // Reset to normal display
      if (errorElement !== null) {
         errorElement.innerHTML = "";
      }
      if (inputElement !== null) {
         inputElement.className = "";
      }
   }
}
 
// Return true if the input value contains only digits (at least one)
function isNumeric(inputId, errorMsg) {
   var inputElement = document.getElementById(inputId);
   var errorElement = document.getElementById(inputId + "Error");
   var inputValue = inputElement.value.trim();
   var isValid = (inputValue.search(/^[0-9]+$/) !== -1);
   showMessage(isValid, inputElement, errorMsg, errorElement);
   return isValid;
}
 function isANumeric(inputId, errorMsg) {
   var inputElement = document.getElementById(inputId);
   var errorElement = document.getElementById(inputId + "Error");
   var inputValue = inputElement.value.trim();
   var isValid = (inputValue.search(/^\d+(\.\d{1,2})?$/) !== -1);
   showMessage(isValid, inputElement, errorMsg, errorElement);
   return isValid;
}
 
// Return true if the input length is between minLength and maxLength
function isLengthMinMax(inputId, errorMsg, minLength, maxLength) {
   var inputElement = document.getElementById(inputId);
   var errorElement = document.getElementById(inputId + "Error");
   var inputValue = inputElement.value.trim();
 var isValid = (inputValue.length >= minLength) && (inputValue.length <= maxLength);
   showMessage(isValid, inputElement, errorMsg, errorElement);
   return isValid;
}
 
// Return true if the input value is a valid email address
// (For illustration only. Should use regexe in production.)
function isValidEmail(inputId, errorMsg) {
   var inputElement = document.getElementById(inputId);
   var errorElement = document.getElementById(inputId + "Error");
   var inputValue = inputElement.value;
   var atPos = inputValue.indexOf("@");
   var dotPos = inputValue.lastIndexOf(".");
   var isValid = (atPos > 0) && (dotPos > atPos + 1) && (inputValue.length > dotPos + 2);
   showMessage(isValid, inputElement, errorMsg, errorElement);
   return isValid;
}


// The "onclick" handler for the "reset" button to clear the display
function clearDisplay() {
   var elms = document.getElementsByTagName("*");  // all tags
   for (var i = 0; i < elms.length; i++) {
      if ((elms[i].id).match(/Error$/)) {  // no endsWith() in JS?
         elms[i].innerHTML = "";
      }
      if (elms[i].className === "error") {  // assume only one class
         elms[i].className = "";
      }
   }
   // Set initial focus
   document.getElementById("account_id").focus();
}
