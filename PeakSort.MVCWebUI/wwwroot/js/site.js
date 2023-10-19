function convertFirstLetterToUpperCase(text)
{
    text.charAt(0).toUpperCase() + text.slice(1);

}

function converToShortDate(dateString) {
    const sortdate = new Date(dateString).toLocaleDateString('en-US');
    return sortdate;
}