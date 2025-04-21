/**
 *@typedef ProfileData
 @property {string} id
* @property {string} name
* @property {string} picture
*/
export class Profile {
  constructor(data) {
    this.id = data.id
    this.name = data.name
    this.picture = data.picture
  }

}

export class Account extends Profile {
  /**
   * @typedef AccountData
   * @property {string} email
   * 
   * @param {AccountData} data
   */
  constructor(data) {
    super(data)
    this.email = data.email
    // TODO add additional properties if needed
  }
}
