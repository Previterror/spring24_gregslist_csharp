export const dev = window.location.origin.includes('localhost')

// NOTE don't forget to change your baseURL if using the dotnet template
export const baseURL = dev ? 'https://localhost:7045' : ''
export const useSockets = false

// TODO change these variables out to your own auth after cloning!
export const domain = 'dev-xx3uaukw42oxjpp7.us.auth0.com'
export const clientId = 'tT0AlUNf86WktlsiRBhWasSin54bs39a'
export const audience = 'https://learning-code.com'