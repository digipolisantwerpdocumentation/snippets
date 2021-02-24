const axios = require("axios");

class BraasService {
    constructor(config) {
        this.axiosInstance = axios.create({
            baseURL: config.baseAddress,
            headers: {
                ApiKey: config.apiKey,
            },
        });
    }

    getOptions(jwt) {
        const options = {
            headers: {},
        };

        if (jwt) {
            options.headers["Dgp-Authorization-For"] = `Bearer ${jwt}`;
        }

        return options;
    }

    async getApplication(applicationId, jwt) {
        const response = await this.axiosInstance.get(`applications/${applicationId}`, this.getOptions(jwt));
        console.log(`getApplication response (${response.status}): ${JSON.stringify(response.data)}`);
        // getApplication response (200): {"application":{"attributes":{"CRUD":"","DOMEIN":null,"LOCATIE":null,"STADSBEDRIJF":"zwijndrecht"},"description":null,"displayName":"testapprc01704-someTenantKey","id":"97df3524-3675-4240-9ffc-36f2a1bd99b6","name":"testapprc01704-someTenantKey","targetApplicationUniqueKey":"testapprc01704","tenantKey":"someTenantKey"}}

        return response.data;
    }

    async getApplicationRoles(applicationId, jwt) {
        const response = await this.axiosInstance.get(`applications/${applicationId}/roles`, this.getOptions(jwt));
        console.log(`getApplicationRoles response (${response.status}): ${JSON.stringify(response.data)}`);
        // getApplicationRoles response (200): {"cursor":126,"roles":[{"attributes":{"BEHEERDER":"","CRUD":"","DOMEIN":null,"LOCATIE":null,"STADSBEDRIJF":null},"description":null,"id":"08364e92-6116-4b1d-8da9-a565d89ad148","name":"Test role rc01704","validFrom":null,"validTo":null}]}

        return response.data;
    }

    async getRoleTeams(roleId, jwt) {
        const response = await this.axiosInstance.get(`roles/${roleId}/teams`, this.getOptions(jwt));
        console.log(`getRoleTeams response (${response.status}): ${JSON.stringify(response.data)}`);
        // getRoleTeams response (200): {"cursor":1557930635498103,"teams":[{"attributes":{"CRUD":null,"DOMEIN":null,"LOCATIE":"test1","STADSBEDRIJF":null},"description":null,"id":"7a1bc4e8-1417-42e6-90ec-e646b9188d00","joinApprovalRequired":false,"name":"Test team rc01704","open":true,"validFrom":null,"validTo":null,"visible":true}]}

        return response.data;
    }

    async getTeamSubjects(teamId, jwt) {
        const response = await this.axiosInstance.get(`teams/${teamId}/subjects`, this.getOptions(jwt));
        console.log(`getTeamSubjects response (${response.status}): ${JSON.stringify(response.data)}`);
        // getTeamSubjects response (200): {"cursor":110668,"members":[{"address":null,"domain":"ICA","email":"Some.User@digipolis.be","externalMutableReference":"rc01704@digant.antwerpen.local","firstname":"Some","id":"3aebe845-0a71-443a-b868-5c4f0e1b84cc","lastname":"User","nickname":null,"owner":true,"type":"mprofiel","username":"rc01704"},{"address":null,"domain":"ICA","email":"Other.User@digipolis.be","externalMutableReference":"rc00992@digant.antwerpen.local","firstname":"Other","id":"29730b11-7056-48c3-80ef-b6de3904455c","lastname":"User","nickname":null,"owner":false,"type":"mprofiel","username":"rc00992"}]}

        return response.data;
    }
}

module.exports = BraasService;
