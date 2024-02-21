import moment from 'moment';

export function isArrayEmpty(obj: any) {
    return obj.length == 0 ? true : false;
}

export function isZero(obj: any) {
    return obj == 0 ? true : false;
}

export function isNull(obj: any) {
    return obj == null ? true : false;
}

export function isUndefined(obj: any) {
    return obj == undefined ? true : false;
}

export function isEmpty(obj: any) {
    return obj == '' ? true : false;
}

export function convertDateToUtc(date: Date | string): string {
    if (!date)
        return '';

    let currentdate = new Date();
    let formattedDate = new Date(date);
    formattedDate.setHours(currentdate.getHours());
    formattedDate.setMinutes(currentdate.getMinutes());
    formattedDate.setSeconds(currentdate.getSeconds());
    let utcDate = moment(formattedDate).utc().toISOString();
    return utcDate;
}

export function convertOnlyDate(date: Date | string): string {
    if (!date)
        return '';

    let currentdate = new Date();
    let formattedDate = new Date(date);
    formattedDate.setHours(currentdate.getHours());
    formattedDate.setMinutes(currentdate.getMinutes());
    formattedDate.setSeconds(currentdate.getSeconds());
    let utcDate = moment(formattedDate).utc().format("YYYY-MM-DD");
    return utcDate;
}


export function compareOnlyTime(startTime: Date | string, currentTime: Date | string, addMins: number): boolean {
    var beginningTime = moment(startTime, 'h:mma');
    var endingTime = beginningTime;
    var currentOtpTime = moment(currentTime, 'h:mma');
    if (addMins)
        endingTime.add(addMins, 'minutes')

    if (currentOtpTime.isBefore(endingTime)) {
        return true;
    }
    else {
        return false;
    }
}

