﻿//run on k6 using environment variable, default using Product resource (lighter query)
//testing order endpoint : k6 run 2_sync_vs_sync_no_track.js -e resource=Order

import http from 'k6/http';
import { check, group, sleep } from 'k6';
import { Trend } from 'k6/metrics';

const baseUrl = 'https://localhost:7154/api';
const resource = __ENV.resource ? __ENV.resource : 'Product'

let respTimeSync = new Trend('resp_time_sync');
let respTimeSyncNoTrack = new Trend('resp_time_sync_notrack');
let iterationDurationSync = new Trend('iteration_duration_sync');
let iterationDurationSyncNoTrack = new Trend('iteration_duration_sync_notrack');

export const options = {
    vus: 10,
    duration: '30s'
};

export default () => {
    group('SyncEndpoint', function () {
        let group1Start = new Date().getTime();

        let res = http.get(`${baseUrl}/${resource}/sync`);

        let group1End = new Date().getTime();

        iterationDurationSync.add(group1End - group1Start);
        respTimeSync.add(res.timings.duration);

        const messageKey = `sync ${resource} status 200`

        check(res, {
            [messageKey]: (r) => r.status === 200,
        });
    });

    group('SyncNoTrackEndpoint', function () {
        let group2Start = new Date().getTime();

        let res = http.get(`${baseUrl}/${resource}/sync-notrack`);

        let group2End = new Date().getTime();

        iterationDurationSyncNoTrack.add(group2End - group2Start);
        respTimeSyncNoTrack.add(res.timings.duration);

        const messageKey = `sync no track ${resource} status 200`

        check(res, {
            [messageKey]: (r) => r.status === 200,
        });
    });

    sleep(0.1);
}

export function handleSummary(data) {
    return {
        stdout: `
        ============
        TEST SUMMARY
        ============

        Iteration Duration(ms) :
        - Sync ${resource} : Avg ${data.metrics.iteration_duration_sync.values.avg.toFixed(2)}, P95 ${data.metrics.iteration_duration_sync.values['p(95)'].toFixed(2)}
        - Sync No Track ${resource} : Avg ${data.metrics.iteration_duration_sync_notrack.values.avg.toFixed(2)}, P95 ${data.metrics.iteration_duration_sync_notrack.values['p(95)'].toFixed(2)}

        Response Time(ms) :
        - Sync ${resource} : Avg ${data.metrics.resp_time_sync.values.avg.toFixed(2)}, P95 ${data.metrics.resp_time_sync.values['p(95)'].toFixed(2)}
        - Sync No Track ${resource} : Avg ${data.metrics.resp_time_sync_notrack.values.avg.toFixed(2)}, P95 ${data.metrics.resp_time_sync_notrack.values['p(95)'].toFixed(2)}
        `
    };
}