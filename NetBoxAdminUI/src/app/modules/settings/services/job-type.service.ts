import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ResponseResult } from "../../../core/models/response-result.model";
import { SearchRequestModel } from "../../../core/models/search-request.model";
import { BaseService } from "../../../core/services/base.service";
import { JobTypeModel } from "../models/job-type.model";

@Injectable({
    providedIn: 'root'
})
export class JobTypeService extends BaseService {

    constructor() {
        super();
    }

    create(jobTypeModel: JobTypeModel): Observable<ResponseResult<JobTypeModel>> {
        return this.post<ResponseResult<JobTypeModel>>('settings/job-types', jobTypeModel);
    }

    getAll(searchModel: SearchRequestModel): Observable<ResponseResult<JobTypeModel[]>> {
        return this.get<ResponseResult<JobTypeModel[]>>(`settings/job-types?pageSize=${searchModel.pageSize}&pageNumber=${searchModel.pageNumber}&searchQuery=${searchModel.searchTerm}`);
    }

    getById(id: string): Observable<ResponseResult<JobTypeModel>> {
        return this.get<ResponseResult<JobTypeModel>>(`settings/job-types/${id}`);
    }

    update(jobTypeModel: JobTypeModel) {
        return this.put(`settings/job-types/${jobTypeModel.id}`, jobTypeModel)
    }

    deleteJobType(id: string): Observable<object> {
        return this.delete(`settings/job-types/${id}`);
    }
}
