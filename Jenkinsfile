pipeline {
  agent any
  stages {
    stage('dev') {
      steps {
        git(url: 'https://github.com/amishra138/IdentityService.git', branch: 'master', poll: true, credentialsId: 'amishra138')
      }
    }

  }
}