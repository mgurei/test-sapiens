% Morti in carica, danno da urto

% v - velocita attacante
% F1 - Formazione attaccante
% F2 - Formazione difensore
% T - Terreno
% t - Tiro
% O - Ostacoli
% HP1 - Vita attaccante
% Hp2 - Vita difensore
% E1 - Esperienza attaccante
% E2 - Esperienza difensore
% M1 - Morale attaccante
% M2 - Morale difensore
% N - Numero attaccanti

function out = MortiUrto(v, F1, F2, T, t, O, HP1, HP2, E1, E2, M1, M2, N )


out = (((F1*2*v/(F2*(T+t+O)))*((HP1*(E1+M1)/2)/(HP2^2)*(E2+M2)/2))*N);

end