import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ResponseResult } from "../../../core/models/response-result.model";
import { SearchRequestModel } from "../../../core/models/search-request.model";
import { BaseService } from "../../../core/services/base.service";
import { ServiceTypeModel } from "../models/service-type.model";

@Injectable({
    providedIn: 'root'
})
export class ServiceTypeService extends BaseService {

    constructor() {
        super();
    }

    create(serviceTypeModel: ServiceTypeModel): Observable<ResponseResult<ServiceTypeModel>> {
        return this.post<ResponseResult<ServiceTypeModel>>('settings/service-types', serviceTypeModel);
    }

    getAll(searchModel: SearchRequestModel): Observable<ResponseResult<ServiceTypeModel[]>> {
        return this.get<ResponseResult<ServiceTypeModel[]>>(`settings/service-types?pageSize=${searchModel.pageSize}&pageNumber=${searchModel.pageNumber}&searchQuery=${searchModel.searchTerm}`);
    }

    getById(id: string): Observable<ResponseResult<ServiceTypeModel>> {
        return this.get<ResponseResult<ServiceTypeModel>>(`settings/service-types/${id}`);
    }

    update(serviceTypeModel: ServiceTypeModel) {
        return this.put(`settings/service-types/${serviceTypeModel.id}`, serviceTypeModel)
    }

    deleteServiceType(id: string): Observable<object> {
        return this.delete(`settings/service-types/${id}`);
    }
}
