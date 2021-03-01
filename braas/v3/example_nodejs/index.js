const BraasService = require("./braasService");
const config = require("./config.json");

(async () => {
    try {
        console.log("Starting BRaaS API example app");

        const service = new BraasService({
            apiKey: config.apiKey,
            baseAddress: config.baseAddress,
        });

        // Example uses JWT from config. In real implementations, you could use the user's JWT
        await service.getApplication(config.applicationId, config.jwt);
        const roles = await service.getApplicationRoles(config.applicationId, config.jwt);
        const teams = await service.getRoleTeams(roles.roles[0].id, config.jwt);
        const subjects = await service.getTeamSubjects(teams.teams[0].id, config.jwt);
    } catch (ex) {
        console.log(`Something went wrong: ${ex.message}`);
    }
})();
