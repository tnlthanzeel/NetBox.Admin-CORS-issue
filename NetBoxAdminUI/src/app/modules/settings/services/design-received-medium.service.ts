import { Injectable } from "@angular/core";
import { Observable, catchError, map } from "rxjs";
import { ResponseResult } from "../../../core/models/response-result.model";
import { SearchRequestModel } from "../../../core/models/search-request.model";
import { BaseService } from "../../../core/services/base.service";
import { JobTypeModel } from "../models/job-type.model";
import { DesignReceivedMediumModel } from "../models/design-received-medium.model";
import { HttpRequest } from "@angular/common/http";

@Injectable({
    providedIn: 'root'
})
export class DesignReceivedMediumService extends BaseService {

    constructor() {
        super();
    }

    create(designMedium: DesignReceivedMediumModel): Observable<any> {
        const formData = new FormData();
        formData.append('image', designMedium.image);
        formData.append('mode', designMedium.mode);

        return this.http.post(this.buildURL('settings/design-sent-by-modes'), formData)
            .pipe(map((response) => (response)), catchError(this.handleServerError));

    }

    getAll(searchModel: SearchRequestModel): Observable<ResponseResult<DesignReceivedMediumModel[]>> {
        return this.get<ResponseResult<DesignReceivedMediumModel[]>>(`settings/design-sent-by-modes?pageSize=${searchModel.pageSize}&pageNumber=${searchModel.pageNumber}&searchQuery=${searchModel.searchTerm}`);
    }

    getById(id: string): Observable<ResponseResult<DesignReceivedMediumModel>> {
        return this.get<ResponseResult<DesignReceivedMediumModel>>(`settings/design-sent-by-modes/${id}`);
    }

    update(designMedium: DesignReceivedMediumModel) {
        const formData = new FormData();
        formData.append('image', designMedium.image);
        formData.append('mode', designMedium.mode);

        return this.http.put(this.buildURL(`settings/design-sent-by-modes/${designMedium.id}`), formData)
            .pipe(map((response) => (response)), catchError(this.handleServerError));
    }

    deleteDesignMedium(id: string): Observable<object> {
        return this.delete(`settings/design-sent-by-modes/${id}`);
    }
}
