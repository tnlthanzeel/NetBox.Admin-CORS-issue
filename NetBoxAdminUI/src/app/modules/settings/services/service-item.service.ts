import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ResponseResult } from "../../../core/models/response-result.model";
import { SearchRequestModel } from "../../../core/models/search-request.model";
import { BaseService } from "../../../core/services/base.service";
import { ServiceModel } from "../models/service.model";


@Injectable({
    providedIn: 'root'
})
export class ServiceItemService extends BaseService {

    constructor() {
        super();
    }

    create(serviceModel: ServiceModel): Observable<ResponseResult<ServiceModel>> {
        return this.post<ResponseResult<ServiceModel>>(`settings/service-types/${serviceModel.serviceTypeId}/services`, serviceModel);
    }

    getAll(serviceTypeId: string, searchModel: SearchRequestModel): Observable<ResponseResult<ServiceModel[]>> {
        return this.get<ResponseResult<ServiceModel[]>>(`settings/service-types/${serviceTypeId}/services?pageSize=${searchModel.pageSize}&pageNumber=${searchModel.pageNumber}&searchQuery=${searchModel.searchTerm}`);
    }

    getById(serviceTypeId: string, id: string): Observable<ResponseResult<ServiceModel>> {
        return this.get<ResponseResult<ServiceModel>>(`settings/service-types/${serviceTypeId}/services/${id}`);
    }

    update(serviceModel: ServiceModel) {
        return this.put(`settings/service-types/${serviceModel.serviceTypeId}/services/${serviceModel.id}`, serviceModel)
    }

    deleteServiceType(serviceTypeId: string, id: string): Observable<object> {
        return this.delete(`settings/service-types/${serviceTypeId}/services/${id}`);
    }
}
