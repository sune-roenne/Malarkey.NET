create table profile (
    profile_id uuid default gen_random_uuid(),
    profile_name varchar(100) not null,
    created_at timestamp default current_timestamp,
    absorbed_by uuid,
    constraint pk_profile primary key (profile_id) 
);

create type provider_type as 
enum('Microsoft','Google', 'Facebook', 'Spotify');


create table profile_identity (
    identity_id uuid default gen_random_uuid(),
    profile_id uuid not null,
    provider provider_type not null,
    provider_id varchar(200) not null,
    identity_name varchar(200) not null,
    preferred_name varchar(200),
    middle_names varchar(200),
    last_name varchar(200),
    email varchar(1000),
    constraint pk_profile_identity primary key (identity_id),
    unique(provider, provider_id),
    constraint fk_profile_identity_prof foreign key (profile_id) references profile(profile_id) on delete cascade
);

create index idx_profile_identity_profid on profile_identity(profile_id);


create type token_type as 
enum('Profile','Identity');

create table token (
  token_id uuid default gen_random_uuid(),
  token_type token_type not null,
  profile_id uuid not null,
  identity_id uuid,
  issued_to varchar(2000) not null,
  issued_at timestamp not null,
  valid_until timestamp not null,
  revoked_at timestamp,
  constraint pk_token primary key (token_id)
);



create table authentication_session (
    session_id bigint primary key generated always as identity,
    state uuid default gen_random_uuid(),
    id_provider provider_type not null,
    nonce varchar(200),
    forwarder varchar(2000),
    code_challenge varchar(2000) not null,
    code_verifier varchar(2000) not null,
    init_time timestamp not null,
    authenticated_time timestamp,
    profile_token_id uuid ,
    identity_token_id uuid ,
    audience varchar(2000) not null,
    scopes varchar(2000),
    forwarder_state varchar(2000),
    unique(state)
);


create table id_provider_token (
    token_id bigint primary key generated always as identity,
    identity_id uuid not null,
    token_string varchar(2000) not null,
    issued_at timestamp not null,
    expires_at timestamp not null,
    refresh_token varchar(2000),
    scopes varchar(2000) not null
);


