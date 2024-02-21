import { Injectable } from "@angular/core";
import { Observable, catchError, map } from "rxjs";
import { ResponseResult } from "../../../core/models/response-result.model";
import { SearchRequestModel } from "../../../core/models/search-request.model";
import { BaseService } from "../../../core/services/base.service";
import { AdvertisementMaterialModel } from "../models/advertisement-material.model";

@Injectable({
    providedIn: 'root'
})
export class AdvertisementMaterialService extends BaseService {

    constructor() {
        super();
    }

    create(adMaterial: AdvertisementMaterialModel): Observable<any> {
        const formData = new FormData();
        formData.append('file', adMaterial.file);

        return this.http.post(this.buildURL('main-display'), formData)
            .pipe(map((response) => (response)), catchError(this.handleServerError));

    }

    getAll(searchModel: SearchRequestModel): Observable<ResponseResult<AdvertisementMaterialModel[]>> {
        return this.get<ResponseResult<AdvertisementMaterialModel[]>>(`main-display?pageSize=${searchModel.pageSize}&pageNumber=${searchModel.pageNumber}&searchQuery=${searchModel.searchTerm}`);
    }

    deleteAdMaterial(id: string): Observable<object> {
        return this.delete(`main-display/${id}`);
    }
}
