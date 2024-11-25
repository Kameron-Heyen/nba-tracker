import { Injectable } from '@angular/core';
import { TeamBranding } from '../DataContracts/TeamBranding';
import { TeamColors } from '../../assets/team-colors';

@Injectable({
  providedIn: 'root',
})
export class BrandingService {
  constructor() {}

  /*
   * Future Improvement: Move this to the backend and store the branding information in a database.
   * Potentially store logos in S3.
   * Caveat: Branding information is not likely to change often, so this is a low priority.
   * */

  getTeamBranding(teamTricode: string): TeamBranding {
    teamTricode = teamTricode.toUpperCase();

    const colors = TeamColors[teamTricode];

    if (!colors) {
      throw new Error(
        `No branding information found for team with tricode: ${teamTricode}`,
      );
    }

    return {
      teamTricode: teamTricode,
      teamLogo: `assets/logos/${teamTricode}.svg`,
      primaryColor: colors.primary,
      secondaryColor: colors.secondary,
    };
  }
}
