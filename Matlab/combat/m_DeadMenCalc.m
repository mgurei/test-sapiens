% @Author: Mihai Gurei <mihaig>
% @Date:   "Sunday, 31st December 2017, 16:57:32"
% @Email:  mihai.gurei@analphabeta.com
% @Project: Sapiens
% @Filename: m_DeadMenCalc.m
% @License: lgpl3

function deadmen = m_DeadMenCalc(pure_damage, armor, paramA, paramD)
    % Attacker
    % pure_damage(1:3): cut, blunt, pierce
    % Defenser
    % armor(1:3): cur, blunt, pierce
    % param: to be undefined

    % Experience
    if paramA == 0
        expA = 1;
    else
        expA = paramA(1);
    end

    if paramD == 0
        expD = 1;
    else
        expD = paramD(1);
    end


    deadmen = m_expRatio(expA, expD) * m_Damage(pure_damage, armor);

end
