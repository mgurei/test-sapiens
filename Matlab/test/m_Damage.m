% @Author: Mihai Gurei <mihaig>
% @Date:   "Saturday, 30th December 2017, 22:32:07"
% @Email:  mihai.gurei@analphabeta.com
% @Project: Sapiens
% @Filename: m_testAttack.m
% @License: lgpl3

%% Simple damaga calculation: weapon DAMAGE/armor DEFENSE
% This considers the three different type of damage (cut, blunt and pierce) and
% devides it by the armor defense specific values

function tot_damage = m_Damage(damage, armor)
    % Attacker
    % damage(1:3): cut, blunt, pierce
    % Defenser
    % armor(1:3): cur, blunt, pierce

    tot_damage = 0;     % output value

    for type = 1:3
        tot_damage = tot_damage + damage(type)/armor(type);
    end

end
%EOF
