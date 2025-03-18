//run on k6 using environment variable
//sync testing : k6 run 0_single_test.js -e method=sync
//async testing : k6 run 0_single_test.js -e method=async
//testing order endpoint : k6 run 0_single_test.js -e method=sync -e resource=Order

import http from 'k6/http';
import { check, sleep } from 'k6';

const config = JSON.parse(open('config.json'));

const baseUrl = config.BASE_URL;
const resource = __ENV.resource ? __ENV.resource : config.DEFAULT_RESOURCE
const method = __ENV.method ? __ENV.method : config.DEFAULT_METHOD;
const endpoint = `/${resource}/${method}`;  

export const options = {
    vus: 10,
    duration: '30s'
};

export default () => {
    let res = http.get(`${baseUrl + endpoint}`);
    const messageKey = `${method} status 200`

    check(res, {
        [messageKey] : (r) => r.status === 200,
    });

    sleep(0.1);
}

